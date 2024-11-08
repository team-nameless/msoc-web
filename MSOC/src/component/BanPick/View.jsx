import Heading from "./Heading";
import { Outlet } from "react-router-dom";
import PropTypes from "prop-types";

export default function View({ currentSession, setCurrentSession, selectedList, setSelectedList }) {
  const headingTitle = currentSession.isBanning
    ? `${currentSession.currentTeam} BANNING`
    : currentSession.isPicking
    ? `${currentSession.currentTeam} PICKING`
    : `FINAL`;

  return (
    <div className="h-full w-full p-10 overflow-x-hidden">
      <Heading headingTitle={headingTitle} />
      <Outlet context={{ currentSession, setCurrentSession, selectedList, setSelectedList }} />
    </div>
  );
}

View.propTypes = {
  currentSession: PropTypes.shape({
    id: PropTypes.number.isRequired,
    currentTeam: PropTypes.string.isRequired,
    selectionMode: PropTypes.string.isRequired,
    isBanning: PropTypes.bool.isRequired,
    isPicking: PropTypes.bool.isRequired,
  }).isRequired,
  setCurrentSession: PropTypes.func.isRequired,
  selectedList: PropTypes.array.isRequired,
  setSelectedList: PropTypes.func.isRequired,
};
