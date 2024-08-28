import axio from "./CustomAxios";

const TrackSelect = (minDiff, maxDiff) => {
	return axio.get(`/api/tracks/select?min_diff=${minDiff}&max_diff=${maxDiff}`);
};

export { TrackSelect };
