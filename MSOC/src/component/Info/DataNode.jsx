export default function DataNode({ value }) {
  return (
    <li className="top-[-5px] m-0 p-0 text-[#00cec9] font-[verdana] text-[14px] list-none relative after:content-[''] block w-[12px] h-[12px] rounded-[50%] bg-[#81ecec] border-[2px] border-[solid] border-[#81ecec] [transition:background-color_.2s_ease]">
      <span className="absolute top-[20px] left-2/4 -translate-x-1/2 translate-y-[0] inline-block text-center w-[100px]">
        {value.time}
      </span>
    </li>
  );
}
