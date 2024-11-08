import SelectComponent from "./SelectComponent";

export default function SelectTable({ currentSlideRenderList, session, trackList, setTrackList }) {
  return (
    <div className="max-w-full">
      <div className="flex flex-col justify-between w-full h-full">
        {currentSlideRenderList.map((row, rowIndex) => (
          <div key={rowIndex} className="flex flex-row justify-start">
            {row.map((item) => (
              <SelectComponent
                key={item.id}
                trackData={item}
                selectionMode={session.selectionMode}
                isSelectingBy={session.currentTeam}
                trackList={trackList}
                setTrackList={setTrackList}
              />
            ))}
          </div>
        ))}
      </div>
    </div>
  );
}
