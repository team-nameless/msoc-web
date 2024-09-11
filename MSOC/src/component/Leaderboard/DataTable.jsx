import React, { useEffect, useState } from "react";
import Data from "../../data/dataPlayer.json";
import Data1 from "../../data/DataSchool.json";
import DataRow from "./DataRow";

export default function DataTable({ isSingle }) {
    const [listData, setListData] = useState([]);

    useEffect(() => {
        const fetchSchool = async () => {
            try {
                const data = isSingle ? Data : Data1;
                let updatedData = data.slice();

                // Fill with placeholder data if less than 10 items
                while (updatedData.length < 10) {
                    updatedData.push({
                        _id: updatedData.length.toString(),
                        name: "No Data",
                        chart1: 0,
                        chart2: 0,
                        dx: 0,
                    });
                }

                setListData(updatedData);
            } catch (error) {
                console.log(error);
            }
        };

        fetchSchool();
    }, [isSingle]);

    return (
        <div className="border border-transparent rounded-xl bg-[url('../public/leaderboardFrame.png')] bg-[length:100%_100%] bg-no-repeat bg-white p-2">
            {/* Added p-4 to apply padding on all sides */}
            <div className="pt-10">
                <table className="w-[100%] h-[100%]">
                    <tbody>
                        {listData.length > 0 ? (
                            listData.map((item, index) => <DataRow key={item._id || index} index={index} item={item} />)
                        ) : (
                            <tr>
                                <td colSpan="7" className="px-6 py-4 text-center text-sm text-gray-500">
                                    {isSingle ? "No player data existed!" : "No school data existed!"}
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
