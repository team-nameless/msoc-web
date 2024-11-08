import React from "react";

export default function DataRow({ index, item }) {
    return (
        <tr key={index} className="table-row max-h-10">
            <td id="school_id" className="px-3 py-2 text-md text-center text-gray-900">
                {item._id}
            </td>
            <td id="name" className="px-2 py-4 text-md text-gray-600">
                {item.name}
            </td>
            <td id="chart1" className="px-2 py-4 text-md text-center text-gray-600">
                {item.chart1}
            </td>
            <td id="chart2" className="px-2 py-4 text-md text-center text-gray-600">
                {item.chart2}
            </td>
            <td id="sum" className="px-2 py-4 text-md text-center text-gray-600">
                {item.chart1 + item.chart2}
            </td>
            <td id="dx" className="px-2 py-4 text-md text-center text-gray-600">
                {item.dx}
            </td>
        </tr>
    );
}
