import { Outlet } from "react-router-dom";
import Header from "./component/Header";

export default function App() {
    return (
        <>
        <head>
        <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'></link>
        <title>MSOC</title>
        </head>
            <Header />
            <body className="max-h-screen">
                <div className="bg-[url('../public/background.png')] bg-cover h-fit">
                    <div className="container mx-auto">
                        <Outlet />
                    </div>
                </div>
            </body>
        </>
    );
}
