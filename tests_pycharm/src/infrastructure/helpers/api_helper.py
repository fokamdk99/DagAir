from src.infrastructure.helpers import request_helper


class ApiHelper():
    def __init__(self, sensors_api_url, policies_api_url, ingestion_node_url, policy_node_url):
        self.sensors_api = ApiService(sensors_api_url)
        self.policies_api = ApiService(policies_api_url)
        self.ingestion_node = ApiService(ingestion_node_url)
        self.policy_node = ApiService(policy_node_url)


class ApiService():
    def __init__(self, url):
        self.base_url = url

    def validate_url(path):
        starts_with_slash = path[0] == "/"
        assert starts_with_slash, f"Invalid endpoint path: {path}. It should begin with a slash."

    def get(self, endpoint_path, auth):
        self.validate_url(endpoint_path)

        path = self.base_url + endpoint_path
        res = request_helper.get_request(path, auth)
        return res

    def post(self, endpoint_path, auth, headers, data):
        self.validate_url(endpoint_path)

        path = self.base_url + endpoint_path
        res = request_helper.post_request(path, auth, headers, data)
        return res