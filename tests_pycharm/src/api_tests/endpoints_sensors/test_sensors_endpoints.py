from src.infrastructure.helpers import request_helper
from src.infrastructure import environment_provider


def test_sensors_endpoints():
    base_url = environment_provider.EnvironmentProvider().get_dagair_sensors()
    sensor_id = 1
    path = f"{base_url}/sensors-api/sensors/{sensor_id}"
    res = request_helper.get_request(path)
    assert res.status_code == 200, f"DagAir.Sensors: endpoint {path} returned status code {res.status_code}"

def test_sensors_endpoints2():
    base_url = environment_provider.EnvironmentProvider().get_dagair_sensors()
    path = f"{base_url}/sensors-api/sensors"
    res = request_helper.get_request(path)
    assert res.status_code == 200, f"DagAir.Sensors: endpoint {path} returned status code {res.status_code}"
