import tensorflow as tf
import numpy as np
import os
import visualkeras
from PIL import ImageFont

train_dataset = tf.keras.preprocessing.image_dataset_from_directory(
    '../prepdata/train', batch_size=54, image_size=(341, 256))
validation_dataset = tf.keras.preprocessing.image_dataset_from_directory('../prepdata/val', batch_size=54,
                                                                         image_size=(341, 256))
test_dataset = tf.keras.preprocessing.image_dataset_from_directory(
    '../prepdata/test', batch_size=54, image_size=(341, 256))



inputs = tf.keras.Input(shape=(341, 256, 3))
x = tf.keras.layers.Rescaling(scale=1.0 / 255)(inputs)

# Convolutional Block
x = tf.keras.layers.Conv2D(filters=64, kernel_size=(3, 3), activation="relu", padding="same")(x)
x = tf.keras.layers.MaxPooling2D(pool_size=(2, 2))(x)

# Convolutional Block
x = tf.keras.layers.Conv2D(filters=128, kernel_size=(3, 3), activation="relu", padding="same")(x)
x = tf.keras.layers.MaxPooling2D(pool_size=(2, 2))(x)

# Convolutional Block
x = tf.keras.layers.Conv2D(filters=256, kernel_size=(3, 3), activation="relu", padding="same")(x)
x = tf.keras.layers.MaxPooling2D(pool_size=(2, 2))(x)

# Convolutional Block
x = tf.keras.layers.Conv2D(filters=512, kernel_size=(3, 3), activation="relu", padding="same")(x)
x = tf.keras.layers.MaxPooling2D(pool_size=(2, 2))(x)

# Fifth Convolutional Block
x = tf.keras.layers.Conv2D(filters=1024, kernel_size=(3, 3), activation="relu", padding="same")(x)
x = tf.keras.layers.MaxPooling2D(pool_size=(2, 2))(x)

# Global Average Pooling
x = tf.keras.layers.GlobalAveragePooling2D()(x)

x = tf.keras.layers.Dense(512, activation="relu")(x)
x = tf.keras.layers.Dropout(0.5)(x)
x = tf.keras.layers.Dense(256, activation="relu")(x)
x = tf.keras.layers.Dropout(0.5)(x)


num_classes = 2
outputs = tf.keras.layers.Dense(num_classes, activation="softmax")(x)

model = tf.keras.Model(inputs=inputs, outputs=outputs)

font = ImageFont.truetype("arial.ttf", 24)
visualkeras.layered_view(model, legend=True, font=font, to_file='../results/modified-arc-custom-model/model1.png', scale_xy=1.0)

model.summary()

filename = os.path.splitext(os.path.basename(__file__))[0]

callbacks = [
    tf.keras.callbacks.ModelCheckpoint(filepath=f'Model/{filename}-end-epoch_' + '{epoch:02d}',
                                       save_freq='epoch')
]

model.compile(optimizer='adam', loss='sparse_categorical_crossentropy', metrics=["accuracy"])
history = model.fit(train_dataset, epochs=15, callbacks=callbacks, shuffle=True, validation_data=validation_dataset)
print(history.history)
np.save(f'History/{filename}-end-history.npy', history.history)

loss, acc = model.evaluate(test_dataset)
print("loss: %.2f" % loss)
print("acc: %.2f" % acc)
