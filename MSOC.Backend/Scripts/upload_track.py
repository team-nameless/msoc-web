import csv
import json
import enum
from http.client import HTTPConnection
from time import sleep


class Difficulty(enum.Enum):
    BASIC = 0
    ADVANCED = 1
    EXPERT = 2
    MASTER = 3
    RE_MASTER = 4


class ChartType(enum.Enum):
    ST = 0
    DX = 1


def send_request(
        const: float,
        ver: str,
        category: str,
        artist_name: str,
        title_name: str,
        cover: str,
        diff_name: Difficulty,
        chart_type: ChartType
) -> None:
    if const == 0.0:
        return

    body = {
        "title": title_name,
        "category": category,
        "artist": artist_name,
        "version": ver,
        "difficulty": diff_name.value,
        "type": chart_type.value,
        "coverImageUrl": cover,
        "constant": const,
    }

    headers = {"Content-type": "application/json"}

    http_handler = HTTPConnection("localhost", 5246, timeout=10000)
    http_handler.request("POST", "/api/admin/add-track", json.dumps(body), headers)

    print(f"Sent {body} to the database!")
    
    sleep(1)


if __name__ == "__main__":
    with (
        open("MSOC raw track list.csv", "r", encoding="utf-8") as f
    ):
        csv_file = csv.reader(f)

        # we skip 2 rows
        next(csv_file)
        next(csv_file)

        while fields := next(csv_file):
            (
                version, cat, artist, title,
                dx_basic, dx_advanced, dx_expert, dx_master, dx_remaster,
                st_basic, st_advanced, st_expert, st_master, st_remaster,
                _, cover_image
            ) = fields

            dx_basic    = float(dx_basic    if dx_basic    != "" else "0.0")
            dx_advanced = float(dx_advanced if dx_advanced != "" else "0.0")
            dx_expert   = float(dx_expert   if dx_expert   != "" else "0.0")
            dx_master   = float(dx_master   if dx_master   != "" else "0.0")
            dx_remaster = float(dx_remaster if dx_remaster != "" else "0.0")

            st_basic    = float(st_basic    if st_basic    != "" else "0.0")
            st_advanced = float(st_advanced if st_advanced != "" else "0.0")
            st_expert   = float(st_expert   if st_expert   != "" else "0.0")
            st_master   = float(st_master   if st_master   != "" else "0.0")
            st_remaster = float(st_remaster if st_remaster != "" else "0.0")

            send_request(dx_basic   , version, cat, artist, title, cover_image, Difficulty.BASIC    , ChartType.DX)
            send_request(dx_advanced, version, cat, artist, title, cover_image, Difficulty.ADVANCED , ChartType.DX)
            send_request(dx_expert  , version, cat, artist, title, cover_image, Difficulty.EXPERT   , ChartType.DX)
            send_request(dx_master  , version, cat, artist, title, cover_image, Difficulty.MASTER   , ChartType.DX)
            send_request(dx_remaster, version, cat, artist, title, cover_image, Difficulty.RE_MASTER, ChartType.DX)

            send_request(st_basic   , version, cat, artist, title, cover_image, Difficulty.BASIC    , ChartType.ST)
            send_request(st_advanced, version, cat, artist, title, cover_image, Difficulty.ADVANCED , ChartType.ST)
            send_request(st_expert  , version, cat, artist, title, cover_image, Difficulty.EXPERT   , ChartType.ST)
            send_request(st_master  , version, cat, artist, title, cover_image, Difficulty.MASTER   , ChartType.ST)
            send_request(st_remaster, version, cat, artist, title, cover_image, Difficulty.RE_MASTER, ChartType.ST)
