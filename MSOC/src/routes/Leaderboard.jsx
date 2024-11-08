import React, { useState } from "react";

import ColumnHeader from "../component/Leaderboard/ColumnHeader";
import DataTable from "../component/Leaderboard/DataTable";

export default function Leaderboard() {
    const [isSingle, setIsSingle] = useState(false);

    return (
        <div className="w-full h-full flex flex-col items-center">
            <div className="w-[90%] h-[80%]">
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
