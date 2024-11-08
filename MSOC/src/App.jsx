import { Outlet } from "react-router-dom";
import Header from "./component/Header";
import Footer from "./component/Footer";

export default function App() {
    return (
        <>
            <head>
                <link href="https://fonts.googleapis.com/css?family=Poppins" rel="stylesheet"></link>
                <title>MSOC</title>
            </head>

            <body className="bg-[url('../public/background.png')] bg-cover h-screen">
                <Header />
                <div className="content container mx-auto pt-24 w-full h-[90%]">
                    <Outlet />
                </div>
                <Footer />
            </body>
        </>
    );
}
