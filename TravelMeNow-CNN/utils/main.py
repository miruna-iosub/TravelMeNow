import base64
import io
import numpy as np
from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
import keras.utils as image
from db import functions
import uvicorn
from tensorflow import keras

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

class ImageData(BaseModel):
    image: str

# model = keras.models.load_model("densenet-final-epoch_13")
model = keras.models.load_model("db/last-model-1-total-end-epoch_15")
# model = tf.keras.models.load_model("last-model-1-total-end-epoch_15")

def prepare_data(actual_label):
    result = functions.get_poi(actual_label)
    info = result[2]
    print(info)
    response = {"name": result[1]}
    print(response)
    if info[-1] == '.':
        info = info[:-1]
    for index, info in enumerate(info.split('.')):
        response[f"info{index}"] = info

    hours_data = functions.get_opening_hours(result[0])
    opening_hours = "Opening hours:\n"
    for day in hours_data:
        if day[1] is None:
            opening_hours += f"{day[0]} Closed\n"
        else:
            opening_hours += f"{day[0]} {day[1]} - {day[2]}\n"

    lines = opening_hours.split('\n')
    max_day_length = max(len(line.split()[0]) for line in lines[1:-1])

    formatted_lines = [lines[0]]
    for line in lines[1:-1]:
        day, hours = line.split(maxsplit=1)
        padded_day = day.ljust(max_day_length)
        formatted_lines.append(f"{padded_day} {hours}")

    formatted_hours = '\n'.join(formatted_lines)

    response["opening_hours"] = formatted_hours
    response["link"] = result[3]
    return response


def get_poi_label(image64):
    threshold = 0.99
    if image64 is None:
        raise ValueError("Image data is missing.")

    # img = image.load_img(io.BytesIO(base64.b64decode(image64)), target_size=(341, 256))
    img = image.load_img(io.BytesIO(base64.b64decode(image64)), target_size=(224, 224))

    img_array = image.img_to_array(img)
    img_array = np.expand_dims(img_array, axis=0)
    preprocessed_img = keras.applications.densenet.preprocess_input(img_array)

    prediction = model.predict(preprocessed_img)
    print(prediction)

    max_value = max(prediction[0])
    print(max_value)
    # prediction[0] = [1 if x == max_value else 0 for x in prediction[0]]

    # predicted_class_index = np.argmax(prediction)

    poi_dict = {
        0: 'ArcDeTriomphe',
        1: 'EiffelTower',
        2: 'LouvreMuseum',
        3: 'MuseedOrsay',
        4: 'NotreDame',
        5: 'NotIdentified'
    }

    if max(prediction[0]) < threshold:
        actual_label = poi_dict[5]
    else:
        print('this is max value')
        print(max(prediction[0]))
        predicted_class_index = np.argmax(prediction)
        actual_label = poi_dict[predicted_class_index]

    # actual_label = poi_dict[predicted_class_index]
    print("Predicted class label:", actual_label)
    return actual_label


@app.post("/image_api")
async def classify_image(data: ImageData):
    try:
        image64 = data.image
        actual_label = get_poi_label(image64)
        print(actual_label)
        response = prepare_data(actual_label)
        print(response)
        return response
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

if __name__ == "__main__":
    uvicorn.run(
        "main:app",
        host="192.168.1.6",
        port=8005,
        ssl_keyfile="../final_ca_private_key.pem",
        ssl_certfile="../final_ca_certificate1.pem"
    )