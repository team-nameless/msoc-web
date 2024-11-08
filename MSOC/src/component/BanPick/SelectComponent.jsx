import React from "react";
import img18 from "../../assets/BanPick/pickingComponentHeader.png";
import { FaBan, FaCheck } from "react-icons/fa";

function arraySet(array, value) {
  return array.map((item) => (item.id === value.id ? value : item));
}

function parseDataList(trackList) {
  let bannedTeam1List = [];
  let bannedTeam2List = [];
  let pickedTeam1List = [];
  let pickedTeam2List = [];

  trackList.forEach((trackData) => {
    if (trackData.banned) {
      trackData.bannedBy === "Team 1" ? bannedTeam1List.push(trackData) : bannedTeam2List.push(trackData);
    }
    if (trackData.picked) {
      trackData.pickedBy === "Team 1" ? pickedTeam1List.push(trackData) : pickedTeam2List.push(trackData);
    }
  });

  return { bannedTeam1List, bannedTeam2List, pickedTeam1List, pickedTeam2List };
}

export default function SelectComponent({ trackData, selectionMode, isSelectingBy, trackList, setTrackList }) {
  const data = parseDataList(trackList);

  const switchPickMode = () => {
    const updatedTrackData = { ...trackData };

    if (selectionMode === "banning") {
      const isBanned = updatedTrackData.banned;

      if (isSelectingBy === "Team 1" && data.bannedTeam1List.length >= 4 && !isBanned) {
        alert("Max ban is 4");
        return;
      }
      if (isSelectingBy === "Team 2" && data.bannedTeam2List.length >= 4 && !isBanned) {
        alert("Max ban is 4");
        return;
      }

      if (updatedTrackData.banned && updatedTrackData.bannedBy !== isSelectingBy) {
        alert("This song is banned, can't select!");
        return;
      }

      // Unban if already banned
      if (isBanned && updatedTrackData.bannedBy === isSelectingBy) {
        updatedTrackData.banned = false;
        updatedTrackData.bannedBy = undefined;
      } else {
        updatedTrackData.banned = true;
        updatedTrackData.bannedBy = isSelectingBy;
      }
    } else if (selectionMode === "picking") {
      const isPicked = updatedTrackData.picked;

      if (
        (isSelectingBy === "Team 1" && data.pickedTeam1List.length >= 1 && !isPicked) ||
        (isSelectingBy === "Team 2" && data.pickedTeam2List.length >= 1 && !isPicked)
      ) {
        alert("Max pick is 1");
        return;
      }

      if (updatedTrackData.banned) {
        alert("This song is banned, can't select!");
        return;
      }

      if (updatedTrackData.picked && updatedTrackData.pickedBy !== isSelectingBy) {
        alert("This song is picked, can't select!");
        return;
      }

      // Unpick if already picked
      if (isPicked && updatedTrackData.pickedBy === isSelectingBy) {
        updatedTrackData.picked = false;
        updatedTrackData.pickedBy = undefined;
      } else {
        updatedTrackData.picked = true;
        updatedTrackData.pickedBy = isSelectingBy;
      }
    }

    setTrackList(arraySet(trackList, updatedTrackData));
  };

  const isBanned = trackData.banned;
  const isPicked = trackData.picked;

  const titleBgColor = isBanned ? "white" : isPicked ? "[#aa61de]" : "[#aa61de]";
  const titleTextColor = isBanned ? "black" : isPicked ? "white" : "white";
  const titleBold = isBanned ? "font-semibold" : "";

  return (
    <div
      className="w-[25%] flex flex-col p-2 cursor-pointer"
      onClick={selectionMode === "none" ? () => {} : switchPickMode}
    >
      <div className="h-max">
        <img src={img18} alt="Header" className="w-full" />
      </div>
      <div className="h-full relative flex flex-col justify-end">
        <img src={trackData.actual_image_url} alt="img18" className="w-auto" />

        {isBanned && (
          <div className="absolute h-full bottom-2 inset-0 bg-gradient-to-t from-white via-white/70 to-transparent"></div>
        )}

        {isPicked && (
          <div className="absolute h-full bottom-2 inset-0 bg-gradient-to-t from-[#aa61de] via-[#aa61de]/30 to-transparent"></div>
        )}

        <div className="absolute flex flex-col items-center justify-end top-0 left-0 w-full h-full">
          {isBanned ? (
            <>
              <FaBan className="text-black text-2xl" />
              <div className="leading-4 text-black">BANNED</div>
            </>
          ) : isPicked ? (
            <>
              <FaCheck className="text-white text-2xl" />
              <div className="leading-4 text-white">PICKED</div>
            </>
          ) : null}
        </div>
      </div>
      <div className={`bg-${titleBgColor} bottom-2 px-6 w-full`}>
        <div className={`line-clamp-1 text-wrap text-center text-lg text-${titleTextColor} ${titleBold}`}>
          {trackData.title}
        </div>
      </div>
    </div>
  );
}
