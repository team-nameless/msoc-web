import { useNavigate, useOutletContext } from "react-router-dom";
import img18 from "../../assets/BanPick/image 18.png";
import SelectTable from "./SelectTable";
import PickedItemCard from "./PickedItemCard";

export default function FinalContent() {
  const { currentSession, selectedList } = useOutletContext();
  const navigate = useNavigate();

  if (!selectedList || selectedList.length === 0) {
    navigate("/banpick/");
    return;
  }

  const onlyPickedList = selectedList.filter((item) => item.picked);
  const rows = [];
  for (let i = 0; i < selectedList.length; i += 4) {
    const row = selectedList.filter((track) => track.banned).slice(i, i + 4);
    rows.push(row);
  }

  return (
    <div className="w-full h-fit">
      <div className="p-4 w-full h-[40%] flex flex-row">
        {onlyPickedList.map((item, index) => (
          <div key={index} className="w-[50%] px-8 h-full flex flex-col justify-end">
            <PickedItemCard id={index} trackData={item} />
          </div>
        ))}
      </div>

      <div className="w-full h-[50%] p-4 px-8">
        <SelectTable currentSlideRenderList={rows} session={currentSession} trackList={selectedList} />
      </div>
    </div>
  );
}
