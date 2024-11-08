import { useState } from "react";
import View from "../component/BanPick/View";

const initialSession = {
  id: 1,
  currentTeam: "Team 1",
  selectionMode: "banning",
  isBanning: true,
  isPicking: false,
};

export default function BanPick() {
  const [selectedList, setSelectedList] = useState([]);
  const [currentSession, setCurrentSession] = useState(initialSession);

  return (
    <div className="h-full w-full px-10 overflow-x-hidden">
      <View
        currentSession={currentSession}
        setCurrentSession={setCurrentSession}
        selectedList={selectedList}
        setSelectedList={setSelectedList}
      />
    </div>
  );
}
