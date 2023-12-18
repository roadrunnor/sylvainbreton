import Swal from "sweetalert2";
import axios from "axios";
import config from "../config/config";
import { Artist } from "../models/Artist"; // Adjust the path as necessary

const API_BASE_URL = config.API_BASE_URL;

const apiService = {
	// Artworks
	getAllArtworks: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/artworks`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	getArtworkById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/artworks/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createArtwork: async (artworkData: any) => {
		try {
			const response = await axios.post(
				`${API_BASE_URL}/artworks`,
				artworkData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updateArtwork: async (id: number, artworkData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/artworks/${id}`,
				artworkData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deleteArtwork: async (id: number) => {
		try {
			const response = await axios.delete(`${API_BASE_URL}/artworks/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// Places
	getAllPlaces: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/places`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	getPlaceById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/places/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createPlace: async (placeData: any) => {
		try {
			const response = await axios.post(`${API_BASE_URL}/places`, placeData);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updatePlace: async (id: number, placeData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/places/${id}`,
				placeData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deletePlace: async (id: number) => {
		try {
			const response = await axios.delete(`${API_BASE_URL}/places/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// Performances
	getAllPerformances: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/performances`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	getPerformanceById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/performances/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createPerformance: async (performanceData: any) => {
		try {
			const response = await axios.post(
				`${API_BASE_URL}/performances`,
				performanceData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updatePerformance: async (id: number, performanceData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/performances/${id}`,
				performanceData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deletePerformance: async (id: number) => {
		try {
			const response = await axios.delete(`${API_BASE_URL}/performances/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// Events
	getAllEvents: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/events`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	getEventById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/events/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createEvent: async (eventData: any) => {
		try {
			const response = await axios.post(`${API_BASE_URL}/events`, eventData);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updateEvent: async (id: number, eventData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/events/${id}`,
				eventData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deleteEvent: async (id: number) => {
		try {
			const response = await axios.delete(`${API_BASE_URL}/events/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// EventArtworks
	getAllEventArtworks: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/eventartworks`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	getEventArtworkById: async (eventId: number, artworkId: number) => {
		try {
			const response = await axios.get(
				`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createEventArtwork: async (eventArtworkData: any) => {
		try {
			const response = await axios.post(
				`${API_BASE_URL}/eventartworks`,
				eventArtworkData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updateEventArtwork: async (
		eventId: number,
		artworkId: number,
		eventArtworkData: any
	) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`,
				eventArtworkData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deleteEventArtwork: async (eventId: number, artworkId: number) => {
		try {
			const response = await axios.delete(
				`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// Images
	getAllImages: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/images`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	getImageById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/images/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createImage: async (imageData: any) => {
		try {
			const response = await axios.post(`${API_BASE_URL}/images`, imageData);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	postImage: async (imageData: ImageData) => {
		try {
			const response = await axios.post(`${API_BASE_URL}/images`, imageData);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updateImage: async (id: number, imageData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/images/${id}`,
				imageData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deleteImage: async (id: number) => {
		try {
			const response = await axios.delete(`${API_BASE_URL}/images/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// Sentences
	getAllSentences: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/sentences`);
			// Ensure that the response is an array before returning
			if (Array.isArray(response.data)) {
				return response.data;
			} else {
				console.error(
					"Expected an array of sentences, but received:",
					response.data
				);
				// Return an empty array if the response is not an array
				return [];
			}
		} catch (error) {
			handleError(error);
			// Return an empty array in case of an error
			return [];
		}
	},
	getSentenceById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/sentences/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createSentence: async (sentenceData: any) => {
		try {
			const response = await axios.post(
				`${API_BASE_URL}/sentences`,
				sentenceData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updateSentence: async (id: number, sentenceData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/sentences/${id}`,
				sentenceData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deleteSentence: async (id: number) => {
		try {
			const response = await axios.delete(`${API_BASE_URL}/sentences/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// Artists
	getAllArtists: async (): Promise<Artist[]> => {
		try {
			const response = await axios.get(`${API_BASE_URL}/artists`);
			return response.data; // Assuming this is an array of Artist
		} catch (error) {
			handleError(error);
			return []; // Return an empty array if there's an error
		}
	},
	getArtistById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/artists/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createArtist: async (artistData: any) => {
		try {
			const response = await axios.post(`${API_BASE_URL}/artists`, artistData);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updateArtist: async (id: number, artistData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/artists/${id}`,
				artistData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deleteArtist: async (id: number) => {
		try {
			const response = await axios.delete(`${API_BASE_URL}/artists/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// DynamicContent
	getAllDynamicContents: async () => {
		try {
			const response = await axios.get(`${API_BASE_URL}/dynamiccontents`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	getDynamicContentById: async (id: number) => {
		try {
			const response = await axios.get(`${API_BASE_URL}/dynamiccontents/${id}`);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	createDynamicContent: async (dynamicContentData: any) => {
		try {
			const response = await axios.post(
				`${API_BASE_URL}/dynamiccontents`,
				dynamicContentData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	updateDynamicContent: async (id: number, dynamicContentData: any) => {
		try {
			const response = await axios.put(
				`${API_BASE_URL}/dynamiccontents/${id}`,
				dynamicContentData
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},
	deleteDynamicContent: async (id: number) => {
		try {
			const response = await axios.delete(
				`${API_BASE_URL}/dynamiccontents/${id}`
			);
			return response.data;
		} catch (error) {
			handleError(error);
		}
	},

	// Ajoutez ici d'autres fonctions pour les autres entités si nécessaire
};

function handleError(error: any) {
	if (axios.isAxiosError(error)) {
		switch (error.response?.status) {
			case 400:
				console.error("Erreur 400 : Requête incorrecte", error.response);
				Swal.fire({
					icon: "error",
					title: "Oops...",
					text: "Votre requête ne peut pas être traitée. Veuillez vérifier vos données.",
				});
				break;
			case 404:
				console.error("Erreur 404 : Ressource non trouvée", error.response);
				Swal.fire({
					icon: "error",
					title: "Non trouvé",
					text: "La ressource demandée n'est pas trouvée.",
				});
				break;
			case 500:
				console.error("Erreur 500 : Erreur interne du serveur", error.response);
				Swal.fire({
					icon: "error",
					title: "Erreur de serveur",
					text: "Un problème est survenu sur le serveur. Veuillez réessayer plus tard.",
				});
				break;
			default:
				console.error("Erreur inattendue", error.response);
				Swal.fire({
					icon: "error",
					title: "Erreur inconnue",
					text: "Une erreur inattendue s'est produite. Veuillez réessayer.",
				});
		}
	} else {
		console.error("Erreur non Axios", error);
		Swal.fire({
			icon: "error",
			title: "Erreur",
			text: "Une erreur inconnue s'est produite.",
		});
	}
}

export default apiService;
