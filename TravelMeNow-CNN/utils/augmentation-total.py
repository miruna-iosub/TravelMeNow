from torch.utils.data import Dataset
import os
from glob import glob
import warnings
import albumentations as A
import cv2
from albumentations.pytorch import ToTensorV2
import uuid
import shutil
import splitfolders

warnings.simplefilter('ignore')

splitfolders.ratio('../final_dataset', ratio=(.8, .1, .1))

# https://www.kaggle.com/code/uraninjo/saving-augmentations-albumentation-tutorial/notebook
class ClassificationDataset(Dataset):
    def __init__(self, images_filepaths, transform=None):
        self.images_filepaths = images_filepaths
        self.transform = transform

    def __len__(self):
        return len(self.images_filepaths)

    def __getitem__(self, idx):
        image_filepath = self.images_filepaths[idx]
        # print(image_filepath)
        image = cv2.imread(image_filepath)
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        class_dict = {'ArcDeTriomphe': 0,
                      'EiffelTower': 1,
                      'LouvreMuseum': 2,
                      'MuseedOrsay': 3,
                      'NotreDame': 4}
        label = class_dict[os.path.normpath(image_filepath).split(os.sep)[-2]]
        if self.transform is not None:
            image = self.transform(image=image)["image"]

        return image, label


class_names = os.listdir('../final_dataset')

datasets = {
    'train': [],
    'val': [],
    'test': []
}

for phase in ['train', 'val', 'test']:
    l = []
    for i in glob(f'./output/{phase}/**/*'):
        l.append(i)
    datasets[phase] = l

train_transform = A.Compose(
    [A.ShiftScaleRotate(shift_limit=0.05, scale_limit=0.05, rotate_limit=15, p=0.5),
     A.RandomBrightnessContrast(p=0.5, brightness_limit=(-0.10, 0.10), contrast_limit=(-0.2, 0.2)),
     A.RGBShift(p=0.5, r_shift_limit=(-10, 10), g_shift_limit=(-10, 10), b_shift_limit=(-10, 10)),
     A.ColorJitter(p=0.4, brightness=(0.8, 1.2), contrast=(0.8, 1.0), saturation=(0.8, 1.0), hue=(-0.05, 0.05)),
     A.HorizontalFlip(),
     A.Normalize(mean=(0.485, 0.456, 0.406), std=(0.229, 0.224, 0.225)),
     ToTensorV2(),
     ]
)

original_transform = A.Compose(
    [A.Normalize(mean=(0.485, 0.456, 0.406), std=(0.229, 0.224, 0.225)),
     ToTensorV2(),
     ]
)

alb_dataset = ClassificationDataset(images_filepaths=datasets['train'], transform=train_transform)
original_dataset = ClassificationDataset(images_filepaths=datasets['train'], transform=original_transform)

dataset_sizes = {x: len(datasets[x]) for x in ['train', 'val']}
class_names = ['ArcDeTriomphe', 'EiffelTower', 'LouvreMuseum', 'MuseedOrsay', 'NotreDame']

try:
    os.mkdir('../final_prepdata')
    os.mkdir('../final_prepdata/train')
    os.mkdir('../final_prepdata/train/ArcDeTriomphe')
    os.mkdir('../final_prepdata/train/EiffelTower')
    os.mkdir('../final_prepdata/train/LouvreMuseum')
    os.mkdir('../final_prepdata/train/MuseedOrsay')
    os.mkdir('../final_prepdata/train/NotreDame')

except IsADirectoryError as e:
    print(e)


def original_save(original_dataset, limit):
    s = {0: 'ArcDeTriomphe',
         1: 'EiffelTower',
         2: 'LouvreMuseum',
         3: 'MuseedOrsay',
         4: 'NotreDame'}
    original_dataset.transform = A.Compose(
        [t for t in original_dataset.transform if not isinstance(t, (A.Normalize, ToTensorV2))])

    for idx in range(limit):
        try:
            image, label = original_dataset[idx]
            cv2.imwrite(f'../final_prepdata/{s[label]}/{str(uuid.uuid4())}.jpg', image)
        except Exception as e:
            print('Error', e)


original_save(original_dataset, dataset_sizes['train'])


def alb_save(alb_dataset, limit):
    s = {0: 'ArcDeTriomphe',
         1: 'EiffelTower',
         2: 'LouvreMuseum',
         3: 'MuseedOrsay',
         4: 'NotreDame'}

    alb_dataset.transform = A.Compose(
        [t for t in alb_dataset.transform if not isinstance(t, (A.Normalize, ToTensorV2))])
    lens = {0: 6500 // 1907, 1: 6500 // 2266, 2: 6500 // 2243, 3: 6500 // 1039, 4: 6500 // 1131}
    for idx in range(limit):
        try:
            image, label = alb_dataset[idx]
            for _ in range(lens[label]):
                cv2.imwrite(f'../final_prepdata/train/{s[label]}/{str(uuid.uuid4())}.jpg', image)
                image, label = alb_dataset[idx]
        except Exception as e:
            print("Error", e)


alb_save(alb_dataset, dataset_sizes['train'])
shutil.move('./output/val', '../final_prepdata/')
shutil.move('./output/test', '../final_prepdata/')
shutil.move('./output/train/ArcDeTriomphe', '../final_prepdata/train/ArcDeTriomphe')
shutil.move('./output/train/EiffelTower', '../final_prepdata/train/EiffelTower')
shutil.move('./output/train/LouvreMuseum', '../final_prepdata/train/LouvreMuseum')
shutil.move('./output/train/MuseedOrsay', '../final_prepdata/train/MuseedOrsay')
shutil.move('./output/train/NotreDame', '../final_prepdata/train/NotreDame')


