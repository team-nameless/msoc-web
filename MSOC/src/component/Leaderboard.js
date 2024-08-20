import React, { useEffect, useState } from "react";
import Data from "../data/dataPlayer.json";

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
	return (
		<div>
			<div className="px-32 py-20">
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
					<tbody>
						{ListPlayer && ListPlayer.length > 0 ? (
							ListPlayer.map((player, index) => (
								<tr key={index} className="border-t border-gray-200">
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
				</table>
			</div>
		</div>
	);
}
