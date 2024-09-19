from tensorflow import keras, math
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
import tensorflow as tf
from sklearn.metrics import confusion_matrix

test_path = '../final_prepdata/test'
test_dataset = tf.keras.preprocessing.image.ImageDataGenerator(
    preprocessing_function=tf.keras.applications.densenet.preprocess_input).flow_from_directory(
    directory=test_path,
    target_size=(
        224, 224),
    classes=[
        'ArcDeTriomphe',
        'EiffelTower',
        'LouvreMuseum',
        'MuseedOrsay',
        'NotreDame'],
    batch_size=16,
    shuffle=False)

model = keras.models.load_model("../models/Model/densenet-final-epoch_01")
loss, acc = model.evaluate(test_dataset)

print("loss: %.2f" % loss)
print("acc: %.2f" % acc)

test_loss = []
test_acc = []
for i in range(1, 16):
    model = keras.models.load_model("../models/Model/densenet-final-epoch_{:0>2d}".format(i))
    loss, acc = model.evaluate(test_dataset)

    test_loss += [loss]
    test_acc += [acc]

    print("loss: %.2f" % loss)
    print("acc: %.2f" % acc)

# history loading
history = np.load("../models/History/densenet-final-history.npy", allow_pickle=True).item()
# history_re = np.load("models/History/model-6(retrain)-history.npy", allow_pickle=True).item()
print(history)

fig, axs = plt.subplots(2, 1, figsize=(15, 15))
fig.tight_layout(pad=8)
axs[0].plot(history['loss'])
axs[0].plot(history['val_loss'])
axs[0].plot(test_loss)
axs[0].title.set_text('Training Loss vs Validation Loss vs Test Loss')
axs[0].set_xlabel('Epochs')
axs[0].set_ylabel('Loss')
axs[0].legend(['Train', 'Val', 'Test'])

axs[1].plot(history['accuracy'])
axs[1].plot(history['val_accuracy'])
axs[1].plot(test_acc)
axs[1].title.set_text('Training Accuracy vs Validation Accuracy vs Test Accuracy')
axs[1].set_xlabel('Epochs')
axs[1].set_ylabel('Accuracy')
axs[1].legend(['Train', 'Val', 'Test'])

plt.savefig('../resultss/densenet-finalS.png')
plt.show()
predictions = model.predict(test_dataset)
predicted_classes = np.argmax(predictions, axis=1)
true_classes = test_dataset.classes

conf_matrix = confusion_matrix(true_classes, predicted_classes)
###########
# Get filenames from the test dataset
filenames = test_dataset.filenames

# Get indices of misclassified images
misclassified_indices = [i for i in range(len(true_classes)) if true_classes[i] != predicted_classes[i]]

# Print filenames of misclassified images
for idx in misclassified_indices:
    print("Misclassified Image:", filenames[idx])

##########
plt.figure(figsize=(8, 6))
sns.heatmap(conf_matrix, fmt='d', cmap='cool', xticklabels=test_dataset.class_indices.keys(), yticklabels=test_dataset.class_indices.keys())
plt.xlabel('Predicted labels')
plt.ylabel('True labels')
plt.title('Confusion Matrix')
plt.savefig(f"../resultss/confusion_matrix-densenet-final.png")
plt.show()
