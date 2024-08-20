import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./component/Header";
import Home from "./component/Home";
import Tournament from "./component/Tournament";
import About from "./component/About";
import Leaderboard from "./component/Leaderboard";

export default function App() {
	return (
		<BrowserRouter>
			<header>
				<Header />
			</header>
			<body>
				<Routes>
					<Route path="/" element={<Home />} />
					<Route path="/tournament" element={<Tournament />} />
					<Route path="/about" element={<About />} />
					<Route path="/leaderboard" element={<Leaderboard />} />
				</Routes>
			</body>
		</BrowserRouter>
	);
}
