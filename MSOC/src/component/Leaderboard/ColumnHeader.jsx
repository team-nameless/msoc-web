export default function ColumnHeader() {
    return (
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
    );
}
