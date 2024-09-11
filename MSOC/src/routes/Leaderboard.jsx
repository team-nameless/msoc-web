import React, { useState } from "react";

import ColumnHeader from "../component/Leaderboard/ColumnHeader";
import DataTable from "../component/Leaderboard/DataTable";

export default function Leaderboard() {
    const [isSingle, setIsSingle] = useState(false);

    return (
        <div className="w-full h-screen flex flex-col items-center pt-20">
            <div className="w-[70%] h-[80%] py-10 pt-[40px]">
                {/* Individual Leaderboard */}
                <DataTable isSingle={isSingle}></DataTable>

                <div className="static w-full h-10 max-h-10 flex justify-end">
                    <button
                        className="rounded-lg h-10 w-[300px] font-semibold"
                        onClick={() => setIsSingle(!isSingle)}
                    >
                        <img
                            src={isSingle ? "../switchToGroup.png" : "../switchToPlayer.png"}
                            alt=""
                            className="object-fit object-left-top"
                        />
                    </button>
                </div>
            </div>
        </div>
    );
}
