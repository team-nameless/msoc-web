import csv
import json
import enum
from http.client import HTTPConnection
from time import sleep


class SchoolType(enum.Enum):
    HighSchool = 3
    University = 4


def send_request(
        name: str,
        school_type: SchoolType,
) -> None:
    body = {
        "name": name,
        "type": school_type.value,
    }

    headers = {"Content-type": "application/json"}

    http_handler = HTTPConnection("localhost", 5246, timeout=10000)
    http_handler.request("POST", "/api/admin/add-school", json.dumps(body), headers)

    print(f"Sent {body} to the database!")

    sleep(1)


if __name__ == "__main__":
    try:
        with (
            open("thpt.csv", "r", encoding="utf-8") as f
        ):
            csv_file = csv.reader(f)

            # skip header
            next(csv_file)

            while fields := next(csv_file):
                (_, name) = fields
                send_request(name.upper(), SchoolType.HighSchool)
    except StopIteration:
        pass

    try:
        with (
            open("daihoc.csv", "r", encoding="utf-8") as f
        ):
            csv_file = csv.reader(f)

            # skip header
            next(csv_file)

            while fields := next(csv_file):
                (_, name) = fields
                send_request(name.upper(), SchoolType.University)
    except StopIteration:
        pass
