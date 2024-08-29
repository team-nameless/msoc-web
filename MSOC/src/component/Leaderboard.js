import React, { useEffect, useState } from "react";
import Data from "../data/dataPlayer.json";
import Data1 from "../data/DataSchool.json";

export default function Leaderboard() {
	const [ListPlayer, setPlayer] = useState([]);

	useEffect(() => {
		const fetchPlayer = async () => {
			try {
				const data = Data;
				setPlayer(data);
			} catch (error) {
				console.log(error);
			}
		};
		fetchPlayer();
	});



	const [ListSchool, setSchool] = useState([]);

	useEffect(() => {
		const fetchSchool = async () => {
			try {
				const data = Data1;
				setSchool(data);
			} catch (error) {
				console.log(error);
			}
		};
		fetchSchool();
	});



	const setToSingle = () => {
		document.getElementById("but").onclick =  setToTeam;
		document.getElementById("but").innerHTML = "Bảng xếp hạng cá nhân";
		console.log("switched to single")  
		
		document.getElementById("single").style.display = "contents";
		document.getElementById("school").style.display = "none";
	};
	const setToTeam = () => {
		document.getElementById("but").onclick = setToSingle;
		document.getElementById("but").innerHTML = "Bảng xếp hạng trường";
		console.log("switched to team")
		
		document.getElementById("single").style.display = "none";
		document.getElementById("school").style.display = "contents";
	}




	return (
		<div className='w-[100%] max-w-[1360px] m-auto min-w-[672px]'>
			<div>
				<button className="rounded-[15px] px-3 py-1 font-semibold border-[black] text-gray-600 bg-[aqua] block ml-auto mr-[125px] mt-[40px] mb-[0]" id="but" onClick={setToTeam}>Bảng xếp hạng cá nhân</button>
			</div>
			<div className="px-32 py-20 pt-[40px]">
				{/* Individual Leaderboard */}
				<table className="w-full bg-white border border-gray-200">
					<thead className="bg-gray-100">
						<tr className="">
							<th className="px-6 py-3 text-center text-lg font-semibold text-gray-600">Rank</th>
							<th className="px-2 py-3 text-left text-lg font-semibold text-gray-600">Name</th>
							<th className="px-2 py-3 text-center text-lg font-semibold text-gray-600">{"Chart 1st (%)"}</th>
							<th className="px-2 py-3 text-center text-lg font-semibold text-gray-600">{"Chart 2nd (%)"}</th>
							<th className="px-2 py-3 text-center text-lg font-semibold text-gray-600">Sum</th>
							<th className="px-2 py-3 text-center text-lg font-semibold text-gray-600">Dx Score</th>
						</tr>
					</thead>
					<tbody id="single" style={{display: "contents"}}>
						{ListPlayer && ListPlayer.length > 0 ? (
							ListPlayer.map((player, index) => (
								<tr key={index} className="border-t border-gray-200 table-row">
									<td className="px-6 py-4 text-md text-center text-gray-900">{player._id}</td>
									<td className="px-2 py-4 text-md text-gray-600">{player.name}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600">{player.chart1}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600">{player.chart2}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600">{player.chart1 + player.chart2}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600">{player.dx}</td>
								</tr>
							))
						) : (
							<tr>
								<td colSpan="7" className="px-6 py-4 text-center text-sm text-gray-500">
									No player available.
								</td>
							</tr>
						)}
						</tbody>
						
						<tbody id="school" style={{display: "none"}} >
						{ListSchool && ListSchool.length > 0 ? (
							ListSchool.map((school, index) => (
								<tr key={index} className="border-t border-gray-200 table-row">
									<td className="px-6 py-4 text-md text-center text-gray-900">{school._id}</td>
									<td className="px-2 py-4 text-md text-gray-600" id="name">{school.name}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600" id="chart1">{school.chart1}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600" id="chart2">{school.chart2}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600" id="sum">{school.chart1 + school.chart2}</td>
									<td className="px-2 py-4 text-md text-center text-gray-600" id="dx">{school.dx}</td>
								</tr>
							))
						) : (
							<tr>
								<td colSpan="7" className="px-6 py-4 text-center text-sm text-gray-500">
									No school available.
								</td>
							</tr>
						)}
					</tbody>
				</table>
			</div>
			<script>
				
			</script>
		</div>
	);
}

