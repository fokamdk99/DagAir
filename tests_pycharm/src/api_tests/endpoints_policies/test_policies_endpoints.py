from src.infrastructure.helpers import request_helper
from src.infrastructure import environment_provider

def test_policies_endpoints():
    base_url = environment_provider.EnvironmentProvider().get_dagair_policies()
    policy_id = 1
    path = f"{base_url}/policies-api/policies/{policy_id}"
    res = request_helper.get_request(path)
    assert res.status_code == 200, f"DagAir.Policies: endpoint {path} returned status code {res.status_code}"
