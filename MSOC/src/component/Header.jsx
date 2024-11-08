import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

const link = [
    { name: "YT_Logo", img: "../Youtube_MSOC.png", link: "" },
    { name: "X_Logo", img: "../x_MSOC.png", link: "" },
    { name: "DC_Logo", img: "../DC_MSOC.png", link: "https://discord.gg/PMVTHDgerp" },
    { name: "FB_Logo", img: "../FB_MSOC.png", link: "" },
];

export default function Header() {
    const [isCompact, setIsCompact] = useState(window.innerWidth / window.screen.width < 0.5);

    const handleResize = () => {
        setIsCompact(window.innerWidth / window.screen.width < 0.5);
    };

    useEffect(() => {
        window.addEventListener("resize", handleResize);
        return () => window.removeEventListener("resize", handleResize);
    }, []);

    // useEffect(() => {
    //     // Đây là nơi bạn có thể thấy giá trị mới của isCompact
    //     console.log("isCompact changed:", isCompact);
    // }, [isCompact]);
    return (
        <header className="fixed w-full min-h-20 max-h-20">
            <div className="container w-full min-h-20 max-h-20 flex justify-center pt-3 pb-1 mx-auto drop-shadow-xl">
                <nav className="flex items-center justify-between p-2 lg:px-8 border-4 rounded-xl border-transparent bg-[#F9C791] w-full drop-shadow-lg">
                    <div className="justify-start max-w-[120px]">
                        <a href="/" className="max-w-[120px]">
                            <img src="../fac_1.png" alt="" className="object-cover" />
                        </a>
                    </div>

                    {isCompact ? (
                        <div className="h-full mr-auto">
                            <button className="h-full flex flex-row text-lg items-center">Dropdown</button>
                        </div>
                    ) : (
                        <div className="h-full mr-auto flex flex-row justify-between">
                            <Link
                                to="/home"
                                className="group text-lg flex flex-col items-center justify-center font-semibold leading-6 text-black w-max px-[10px]"
                            >
                                <div className="w-full mt-[5px]">Trang Chủ</div>
                                <span className="w-0 min-h-[2px] bg-black group-hover:min-w-full transition duration-300 mt-[2px] rounded-xl"></span>
                            </Link>
                            <Link
                                to="/info"
                                className="group text-lg flex flex-col items-center justify-center font-semibold leading-6 text-black w-max px-[10px]"
                            >
                                <div className="w-full mt-[5px]">Thông Tin</div>
                                <span className="w-0 min-h-[2px] bg-black group-hover:min-w-full transition duration-300 mt-[2px] rounded-xl"></span>
                            </Link>
                            <Link
                                to="/tournament"
                                className="group text-lg flex flex-col items-center justify-center font-semibold leading-6 text-black w-max px-[10px]"
                            >
                                <div className="w-full mt-[5px]">Tounament</div>
                                <span className="w-0 min-h-[2px] bg-black group-hover:min-w-full transition duration-300 mt-[2px] rounded-xl"></span>
                            </Link>
                            <Link
                                to="/leaderboard"
                                className="group text-lg flex flex-col items-center justify-center font-semibold leading-6 text-black w-max px-[10px]"
                            >
                                <div className="w-full mt-[5px]">Xếp hạng</div>
                                <span className="w-0 min-h-[2px] bg-black group-hover:min-w-full transition duration-300 mt-[2px] rounded-xl"></span>
                            </Link>
                            <Link
                                to="/about"
                                className="group text-lg flex flex-col items-center justify-center font-semibold leading-6 text-black w-max px-[10px]"
                            >
                                <div className="w-full mt-[5px]">Nhân sự</div>
                                <span className="w-0 min-h-[2px] bg-black group-hover:min-w-full transition duration-300 mt-[2px] rounded-xl"></span>
                            </Link>

                        </div>
                    )}

                    <div className="flex flex-row">
                        {link.map((component) => (
                            <a
                                id={component.name}
                                href={component.link}
                                alt={component.name}
                                className="w-[35px] mx-[10px] px-[3px] flex items-center"
                            >
                                <img className="" src={component.img} alt="YT_Logo" />
                            </a>
                        ))}
                    </div>
                </nav>
            </div>
        </header>
    );
}
