from create_health_check_tests import create_health_check_tests
from src.infrastructure.helpers.api_helper import ApiHelper


def test_sensors_api_healthchecks():
    api_helper = ApiHelper()
    create_health_check_tests(api_helper.sensors_api)