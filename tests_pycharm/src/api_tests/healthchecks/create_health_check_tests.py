def create_health_check_tests(api):
    path = "/system/health/ready"
    res = api.get(path)

    assert res.status_code == 204, f"Service {api.service_name} is not ready"

    path = "/system/health/live"
    res = api.get(path)

    assert res.status_code == 204, f"Service {api.service_name} is not live"
