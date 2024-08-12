import React from "react";
import { FaDiscord } from "react-icons/fa6";

export default function Header() {
	return (
		<header className="bg-orange-400">
			<nav className="mx-auto flex items-center justify-between p-4 lg:px-8">
				<div className="justify-start">
					<a href="/" className="-m-1.5 p-1.5">
						<span>MSOC</span>
					</a>
				</div>
				<div className="hidden lg:flex flex-1 justify-center space-x-20">
					<a href="/" className="text-xl font-semibold leading-6 text-white">
						Trang Chủ
					</a>
					<a href="/" className="text-xl font-semibold leading-6 text-white">
						Thông Tin
					</a>
					<a href="/" className="text-xl font-semibold leading-6 text-white">
						Xếp hạng nhóm
					</a>
					<a href="/" className="text-xl font-semibold leading-6 text-white">
						Xếp hạng cá nhân
					</a>
					<a href="/" className="text-xl font-semibold leading-6 text-white">
						Nhân sự
					</a>
				</div>
				<div className=" justify-end ">
					<button className="flex bg-blue-600 rounded-lg px-1 py-1 w-28 space-x-2 items-center justify-center">
						<FaDiscord className="text-white jutsify-center text-xl" />
						<a href="/" className="text-lg font-semibold leading-6 text-white justify-center">
							Discord
						</a>
					</button>
				</div>
			</nav>
		</header>
	);
}
