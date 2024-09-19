#inceptionV3
import warnings
import os
import tensorflow as tf
import numpy as np
import matplotlib.pyplot as plt
import visualkeras
from PIL import ImageFont
from keras.layers import BatchNormalization, Flatten, Dropout
from tensorflow.python.keras.layers import ZeroPadding2D

warnings.simplefilter(action='ignore', category=FutureWarning)

inceptionV3_model = tf.keras.applications.InceptionV3(weights='imagenet', include_top=False, input_shape=(299, 299, 3))

x = inceptionV3_model.output
x = tf.keras.layers.GlobalAveragePooling2D()(x)

x = tf.keras.layers.Dense(512, activation='relu')(x)

predictions = tf.keras.layers.Dense(2, activation='softmax')(x)

model = tf.keras.models.Model(inputs=inceptionV3_model.input, outputs=predictions)

font = ImageFont.truetype("arial.ttf", 90)

visualkeras.layered_view(model, legend=True, font=font, to_file='../results/third-model-inceptionv3/model.png', spacing=1, padding=30,
                         type_ignore=[ZeroPadding2D, BatchNormalization, Flatten, Dropout])

print(model.summary())
for layer in inceptionV3_model.layers:
    layer.trainable = False

train_path = '../prepdata/train'
val_path = '../prepdata/val'
test_path = '../prepdata/test'

train_batches = tf.keras.preprocessing.image.ImageDataGenerator(
    preprocessing_function=tf.keras.applications.inception_v3.preprocess_input).flow_from_directory(
    directory=train_path,
    target_size=(299, 299),
    classes=[
        'EiffelTower',
        'NotreDame'],
    batch_size=16)

val_batches = tf.keras.preprocessing.image.ImageDataGenerator(
    preprocessing_function=tf.keras.applications.inception_v3.preprocess_input).flow_from_directory(
    directory=val_path,
    target_size=(
        299, 299),
    classes=[
        'EiffelTower',
        'NotreDame'],
    batch_size=16)

test_batches = tf.keras.preprocessing.image.ImageDataGenerator(
    preprocessing_function=tf.keras.applications.inception_v3.preprocess_input).flow_from_directory(
    directory=test_path,
    target_size=(
        299, 299),
    classes=[
        'EiffelTower',
        'NotreDame'],
    batch_size=16,
    shuffle=False)

images, labels = next(train_batches)


def plot_images(images_arr):
    fig, axes = plt.subplots(4, 4, figsize=(7, 7))
    axes = axes.flatten()
    for img, ax in zip(images_arr, axes):
        ax.imshow(img)
        ax.axis('off')
    plt.tight_layout()
    plt.savefig('../results/third-model-inceptionv3-preprocess1.png')
    plt.show()


plot_images(images)

filename = os.path.splitext(os.path.basename(__file__))[0]

callbacks = [
    tf.keras.callbacks.ModelCheckpoint(filepath=f'Model/{filename}-epoch_' + '{epoch:02d}',
                                       save_freq='epoch')
]

model.compile(optimizer=tf.keras.optimizers.Adam(learning_rate=0.0001), loss='categorical_crossentropy',
              metrics=['accuracy'])

history = model.fit(x=train_batches,
                    steps_per_epoch=len(train_batches),
                    validation_data=val_batches,
                    validation_steps=len(val_batches),
                    callbacks=callbacks,
                    epochs=15,
                    verbose=2
                    )

print(history.history)
np.save(f'History/{filename}-history.npy', history.history)

loss, acc = model.evaluate(test_batches)
print("loss: %.2f" % loss)
print("acc: %.2f" % acc)
