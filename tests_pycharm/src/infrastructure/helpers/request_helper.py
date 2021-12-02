import requests
from urllib3.util.retry import Retry
from requests.adapters import HTTPAdapter


def get_request(path, auth=None, total_number_of_retries=5, backoff_factor=0.2):
    s = requests.Session()

    retries = Retry(total=total_number_of_retries,
                    backoff_factor=backoff_factor)

    s.mount('http://', HTTPAdapter(max_retries=retries))

    res = s.get(path, auth=auth)
    return res


def post_request(path, auth=None, headers=None, data=None, total_number_of_retries=5, backoff_factor=0.2):
    s = requests.Session()

    retries = Retry(total=total_number_of_retries,
                    backoff_factor=backoff_factor)

    s.mount('http://', HTTPAdapter(max_retries=retries))

    res = s.post(path, auth=auth, headers=headers, data=data)
    return res


def put_request(path, auth=None, headers=None, data=None, total_number_of_retries=5, backoff_factor=0.2):
    s = requests.Session()

    retries = Retry(total=total_number_of_retries,
                    backoff_factor=backoff_factor)

    s.mount('http://', HTTPAdapter(max_retries=retries))

    res = s.put(path, auth=auth, headers=headers, data=data)
    return res


def delete_request(path, auth=None, total_number_of_retries=5, backoff_factor=0.2):
    s = requests.Session()

    retries = Retry(total=total_number_of_retries,
                    backoff_factor=backoff_factor)

    s.mount('http://', HTTPAdapter(max_retries=retries))

    res = s.delete(path, auth=auth)
    return res
