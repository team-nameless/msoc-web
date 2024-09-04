import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./component/Header";
import Home from "./component/Home";
import Tournament from "./component/Tournament";
import About from "./component/About";
import Leaderboard from "./component/Leaderboard";
import Info from "./component/Info";

export default function App() {
	return (
		<BrowserRouter>
			<header>
				<Header />
			</header>
			<body>
				<Routes>
					<Route path="/" element={<Home />} />
					<Route path="/info" element={<Info />} />
					<Route path="/tournament" element={<Tournament />} />
					<Route path="/about" element={<About />} />
					<Route path="/leaderboard" element={<Leaderboard />} />
				</Routes>
			</body>
		</BrowserRouter>
	);
}
