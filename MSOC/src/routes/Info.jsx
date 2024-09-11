import React, { useEffect, useState } from "react";
import DataNode from "../component/Info/DataNode";

const data = [
    { id: "1", name: "chit Tus", time: "30/9/2969" },
    { id: "2", name: "Chit tus", time: "1/10/2969" },
    { id: "3", name: "Chit tus", time: "2/10/2969" },
    { id: "4", name: "Chit tus", time: "3/10/2969" },
    { id: "5", name: "Chit tus", time: "4/10/2969" },
    { id: "6", name: "Chit tus", time: "5/10/2969" },
    { id: "7", name: "Chit tus", time: "6/10/2969" },
    { id: "8", name: "Chit tus", time: "7/10/2969" },
];

export default function Home() {
    const [listData, setListData] = useState([]);

    useEffect(() => {
        const fetchSchool = async () => {
            try {
                setListData(data);
            } catch (error) {
                console.log(error);
            }
        };
        fetchSchool();
    }, []);

    return (
        <div className="w-full h-screen items-center place-content-center pt-20">
            <div className="bg-[#1c1b1a] w-full h-3/5 top-0 mx-auto mt-[max(-10%,-100px)] border-2 rounded-xl border-transparent">
                <div className="flex items-center justify-center mt-[0px] top-[40px] py-10 pr-12 pl-12">
                    <ol className="flex w-[90%] justify-between relative after:content-[''] h-[3px] bg-[#81ecec] top-2/4 left-[0] translate-x-[0] -translate-y-1/2">
                        {listData.map((value, index) => (
                            <DataNode key={value.id} value={value} />
                        ))}
                    </ol>
                    <li className="top-[-2px] m-0 p-0 text-[#00cec9] font-[verdana] text-[14px] list-none relative after:content-[''] block w-[12px] h-[12px] rounded-[50%] bg-[#81ecec] border-[2px] border-[solid] border-[#81ecec] [transition:background-color_.2s_ease]">
                        <span className="absolute top-[19px] left-2/4 -translate-x-1/2 translate-y-[0] inline-block text-center w-[100px]">
                            8/10/2969 sv hidro tra luong
                        </span>
                    </li>
                </div>
            </div>
        </div>
    );
}
