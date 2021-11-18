from src.infrastructure.helpers import request_helper
from src.infrastructure.environment_provider import EnvironmentProvider


class ApiHelper:
    def __init__(self):
        env_provider = EnvironmentProvider()
        configuration = env_provider.get_configuration()
        self.sensors_api = ApiService(configuration["DAGAIR_SENSORS"], "sensors_api")
        self.policies_api = ApiService(configuration["DAGAIR_POLICIES"], "policies_api")
        self.ingestion_node = ApiService(configuration["DAGAIR_INGESTION_NODE"], "ingestion_node")
        self.policy_node = ApiService(configuration["DAGAIR_POLICYNODE"], "policy_node")


class ApiService:
    def __init__(self, url, service_name):
        self.base_url = url
        self.service_name = service_name

    def validate_url(self, path):
        starts_with_slash = path[0] == "/"
        assert starts_with_slash, f"Invalid endpoint path: {path}. It should begin with a slash."

    def get(self, endpoint_path, auth=None):
        self.validate_url(endpoint_path)

        path = self.base_url + endpoint_path
        res = request_helper.get_request(path, auth)
        return res

    def post(self, endpoint_path, auth = None, headers = None, data = None):
        self.validate_url(endpoint_path)

        path = self.base_url + endpoint_path
        res = request_helper.post_request(path, auth, headers, data)
        return res