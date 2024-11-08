import React, { useState, useEffect } from "react";
import Data from "../data/data.json";

export default function About() {
    const [listStaff, setlistStaff] = useState([]);

    //fetch data
    useEffect(() => {
        const fetchStaff = async () => {
            try {
                const data = Data;
                setlistStaff(data);
            } catch (error) {
                console.log(error);
            }
        };
        fetchStaff();
    }, []);

    return (
        <div className="px-1">
            <div className="bg-gray-800">
                <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-6 p-10 items-center place-content-center">
                    {listStaff && listStaff.length > 0 ? (
                        listStaff.map((staff, index) => (
                            <div key={index} className=" w-50 h-50 text-center mb-20">
                                <img src={staff.img} className="w-48 mb-4 m-auto" />
                                <h3 className="text-white text-xl font-semibold mb-2">{staff.name}</h3>
                                <div className="text-white text-lg font-semibold mb-2">{staff.role}</div>
                            </div>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="7" className="px-6 py-4 text-center text-sm text-gray-500">
                                Nhân sự đang được cập nhật.
                            </td>
                        </tr>
                    )}
                </div>
            </div>
        </div>
    );
}
