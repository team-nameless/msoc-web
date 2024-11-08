export default function Heading({ headingTitle }) {
    return (
        <div className="w-full h-[10%] p-3 text-lg flex flex-row justify-start">
            <div className="w-[20%] bg-gradient-to-r from-black via-black/70 to-transparent"></div>
            <div className="w-[40%] flex items-center justify-center text-center font-sans font-bold text-4xl">{headingTitle}</div>
            <div className="w-[20%] bg-gradient-to-l from-black via-black/70 to-transparent"></div>
        </div>
    );
}
