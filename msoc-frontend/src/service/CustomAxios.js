import axios from "axios";

//Port to connect API BE
const Port = 3000;

const instance = axios.create({
	baseURL: `https://localhost:${Port}/`,
});

instance.interceptors.request.use(
	function (config) {
		// Do something before request is sent
		if (config.auth !== false) {
			const token = localStorage.getItem("token");
			if (token) {
				config.headers.Authorization = "Bearer " + token;
			}
		}
		return config;
	},
	function (error) {
		// Do something with request error
		return Promise.reject(error);
	}
);

// Add a response interceptor
instance.interceptors.response.use(
	function (response) {
		// Any status code that lie within the range of 2xx cause this function to trigger
		// Do something with response data
		return response.data ? response.data : { statusCode: response.status };
	},
	function (error) {
		let res = [];
		if (error.response) {
			res.data = error.response.data;
			res.status = error.response.status;
			res.headers = error.response.headers;
		} else if (error.request) {
			console.log(">>> check error: ", error.request);
		} else {
			console.log("Error", error.Message);
		}
		// Any status codes that falls outside the range of 2xx cause this function to trigger
		// Do something with response error
		return res;
	}
);

export default instance;
