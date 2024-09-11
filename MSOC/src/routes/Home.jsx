import React from "react";

export default function Home() {
    return (
        <div className="w-full h-screen flex flex-col justify-between pt-20">
            <div className="w-full flex justify-center min-h-60 my-auto ">
                <img alt="Logo" src="../fac_1.png" className="w-[600px]" />
            </div>
            <div className="w-full flex justify-center p-2">
                <img src="../collab.png" alt="" className="w-1/2" />
            </div>
        </div>
    );
}
