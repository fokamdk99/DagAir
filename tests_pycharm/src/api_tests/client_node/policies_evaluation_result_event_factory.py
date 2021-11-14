import jsonpickle
import json


class PoliciesEvaluationResultEvent:
    def __init__(self, temperature_status, illuminance_status, humidity_status, message):
        self.TemperatureStatus = temperature_status
        self.IlluminanceStatus = illuminance_status
        self.HumidityStatus = humidity_status
        self.Message = message

    def to_json(self):
        entity = jsonpickle.encode(self)
        entity_dict = json.loads(entity)
        del entity_dict["py/object"]
        return json.dumps(entity_dict)
