import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./component/Header";
import Home from "./component/Home";

export default function App() {
	return (
		<BrowserRouter>
			<header>
				<Header />
			</header>
			<body>
				<Routes>
					<Route path="/" element={<Home />} />
				</Routes>
			</body>
		</BrowserRouter>
	);
}
