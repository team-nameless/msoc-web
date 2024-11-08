import banPickImage from "../../assets/BanPick/pickingComponentHeader.png";

export default function PickedItemCard({ trackData }) {
  return (
    <div className="relative h-full flex justify-start">
      <div className="h-full w-[70%] shadow-[20px_20px_2px_-5px_rgba(0,0,0,0.4)]">
        <div className="w-full">
          <img src={banPickImage} alt="Header" />
        </div>
        <div className="relative w-full aspect-square">
          <img className="w-full aspect-square" src={trackData.actual_image_url} alt="Description" />
          <div className="relative">
            <div className="absolute bottom-0 right-[-45%] w-[45%] h-fit flex flex-row bg-[#aa61de] text-white shadow-[15px_15px_2px_0px_rgba(0,0,0,0.4)]">
              <div className="flex items-end">
                <h1 className="text-end text-2xl leading-12">LVL.</h1>
              </div>
              <div className="flex items-end ml-2">
                <h1 className="text-5xl font-bold leading-12">12.5</h1>
              </div>
            </div>
          </div>
        </div>

        <div className="h-fit w-full px-[10%] bottom-0 bg-[#aa61de]">
          <div className="line-clamp-1 text-wrap text-2xl text-center text-white font-semibold">{trackData.title}</div>
        </div>
      </div>
    </div>
  );
}
