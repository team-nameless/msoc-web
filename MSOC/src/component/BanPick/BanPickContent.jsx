import React, { useState, useCallback } from "react";
import SelectTable from "./SelectTable";
import { useNavigate, useOutletContext } from "react-router-dom";

import trackData from "../../data/tracks.json";

function usePagination(data, itemsPerPage) {
  const [currentIndex, setCurrentIndex] = useState(0);

  const nextSlide = () => {
    setCurrentIndex((prevIndex) => Math.min(prevIndex + 1, Math.ceil(data.length / itemsPerPage) - 1));
  };

  const prevSlide = () => {
    setCurrentIndex((prevIndex) => Math.max(prevIndex - 1, 0));
  };

  const displayedData = data.slice(currentIndex * itemsPerPage, (currentIndex + 1) * itemsPerPage);

  return { displayedData, nextSlide, prevSlide, currentIndex };
}

function getRandomElements(array, count) {
  const shuffled = array.sort(() => 0.5 - Math.random());
  return shuffled.slice(0, count);
}

function parseDataList(trackList) {
  let bannedTeam1List = [];
  let bannedTeam2List = [];
  let pickedTeam1List = [];
  let pickedTeam2List = [];

  trackList.forEach((songData) => {
    if (songData.banned) {
      songData.bannedBy === "Team 1" ? bannedTeam1List.push(songData) : bannedTeam2List.push(songData);
    }
    if (songData.picked) {
      songData.pickedBy === "Team 1" ? pickedTeam1List.push(songData) : pickedTeam2List.push(songData);
    }
  });

  return { bannedTeam1List, bannedTeam2List, pickedTeam1List, pickedTeam2List };
}

export default function BanPickContent() {
  const { currentSession, setCurrentSession, setSelectedList } = useOutletContext();

  const [trackList, setTrackList] = useState(getRandomElements(trackData, 10));
  const navigate = useNavigate();

  const itemsPerPage = 8;
  const { displayedData, nextSlide, prevSlide } = usePagination(trackList, itemsPerPage);

  const switchPhase = useCallback(() => {
    const newSession = { ...currentSession };
    const data = parseDataList(trackList);

    if (currentSession.selectionMode === "none") {
      alert("Done");
      return;
    }

    if (newSession.isBanning) {
      if (newSession.currentTeam === "Team 1") newSession.currentTeam = "Team 2";
      else {
        newSession.currentTeam = "Team 1";
        newSession.selectionMode = "picking";
        newSession.isBanning = false;
        newSession.isPicking = true;
      }
    } else if (newSession.isPicking) {
      if (
        (data.pickedTeam1List.length < 1 && currentSession.currentTeam === "Team 1") ||
        (data.pickedTeam2List.length < 1 && currentSession.currentTeam === "Team 2")
      ) {
        alert("No track seleted !");
        return;
      }

      if (newSession.currentTeam === "Team 1") newSession.currentTeam = "Team 2";
      else {
        newSession.isPicking = false;
        setSelectedList(trackList);
        navigate("/banpick/final");
      }
    }
    setCurrentSession(newSession);
  }, [currentSession, trackList]);

  // Chia dữ liệu thành 2 hàng
  const rows = [];
  for (let i = 0; i < displayedData.length; i += 4) {
    const row = displayedData.slice(i, i + 4);
    rows.push(row);
  }

  if (rows.length < 2) rows.push([]);
  return (
    <div className="w-full h-fit flex flex-col items-center">
      <div className="h-full w-full flex flex-col">
        <SelectTable
          currentSlideRenderList={rows}
          session={currentSession}
          trackList={trackList}
          setTrackList={setTrackList}
        />
        <div className="w-full h-[10%] flex justify-center my-4">
          <button className="w-[80px] h-[60%]" onClick={prevSlide}>
            Prev
          </button>
          <button className="w-[80px] h-[60%]" onClick={nextSlide}>
            Next
          </button>
        </div>
      </div>

      <div className="w-full h-fit p-4 flex justify-end">
        <button
          type="button"
          className="h-[40px] w-[8%] px-2 border border-slate-300 rounded-md bg-white hover:bg-slate-300"
          onClick={switchPhase}
          aria-label="Submit Phase"
        >
          Submit
        </button>
      </div>
    </div>
  );
}
