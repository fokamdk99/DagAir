import jsonpickle


class MeasurementSentEvent:
    def __init__(self, temperature, illuminance, humidity, sensor_id):
        self.Temperature = temperature
        self.Illuminance = illuminance
        self.Humidity = humidity
        self.sensor_id = sensor_id

    def to_json(self):
        return jsonpickle.encode(self)


def generate_measurement_sent_event_json():
    measurement_sent_event = MeasurementSentEvent(20, 120, 0.45, "1")
    new_measurement = str(measurement_sent_event.Temperature) + ";" \
                      + str(measurement_sent_event.Illuminance) + ";" \
                      + str(measurement_sent_event.Humidity) + ";" + measurement_sent_event.sensor_id
    return new_measurement
