from keras.applications.vgg19 import VGG19
from keras.models import Model
import tensorflow as tf
import os
import numpy as np
import visualkeras
from PIL import ImageFont

base_model = VGG19(weights='imagenet', include_top=True, input_shape=(224, 224, 3))

for layer in base_model.layers:
    layer.trainable = False

train_path = '../prepdata/train'
val_path = '../prepdata/val'
test_path = '../prepdata/test'

train_batches = tf.keras.preprocessing.image.ImageDataGenerator(
    preprocessing_function=tf.keras.applications.vgg19.preprocess_input).flow_from_directory(directory=train_path,
                                                                                             target_size=(224, 224),
                                                                                             classes=[
                                                                                                 'EiffelTower',
                                                                                                 'NotreDame'],
                                                                                             batch_size=64)

val_batches = tf.keras.preprocessing.image.ImageDataGenerator(
    preprocessing_function=tf.keras.applications.vgg19.preprocess_input).flow_from_directory(directory=val_path,
                                                                                             target_size=(224, 224),
                                                                                             classes=[
                                                                                                 'EiffelTower',
                                                                                                 'NotreDame'],
                                                                                             batch_size=64)

test_batches = tf.keras.preprocessing.image.ImageDataGenerator(
    preprocessing_function=tf.keras.applications.vgg19.preprocess_input).flow_from_directory(directory=test_path,
                                                                                             target_size=(224, 224),
                                                                                             classes=[
                                                                                                 'EiffelTower',
                                                                                                 'NotreDame'],
                                                                                             batch_size=64,
                                                                                             shuffle=False)


filename = os.path.splitext(os.path.basename(__file__))[0]

callbacks = [
    tf.keras.callbacks.ModelCheckpoint(filepath=f'Model/{filename}-epoch_' + '{epoch:02d}',
                                       save_freq='epoch')
]

model = base_model
font = ImageFont.truetype("arial.ttf", 48)
visualkeras.layered_view(model, legend=True, font=font, to_file='../results/vegegel.png', spacing=25, padding=30)

# model.compile(optimizer=tf.keras.optimizers.Adam(learning_rate=0.0001), loss='categorical_crossentropy',
#               metrics=['accuracy'])
#
# history = model.fit(x=train_batches,
#                     steps_per_epoch=len(train_batches),
#                     validation_data=val_batches,
#                     validation_steps=len(val_batches),
#                     callbacks=callbacks,
#                     epochs=10,
#                     verbose=2
#                     )
#
# print(history.history)
# np.save(f'History/{filename}-history.npy', history.history)
#
# loss, acc = model.evaluate(test_batches)
# print("loss: %.2f" % loss)
# print("acc: %.2f" % acc)
