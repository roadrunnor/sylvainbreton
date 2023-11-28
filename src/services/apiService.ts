import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000/api';

const apiService = {
  // Artworks
  getAllArtworks: async () => axios.get(`${API_BASE_URL}/artworks`),
  getArtworkById: async (id: number) => axios.get(`${API_BASE_URL}/artworks/${id}`),
  createArtwork: async (artworkData: any) => axios.post(`${API_BASE_URL}/artworks`, artworkData),
  updateArtwork: async (id: number, artworkData: any) => axios.put(`${API_BASE_URL}/artworks/${id}`, artworkData),
  deleteArtwork: async (id: number) => axios.delete(`${API_BASE_URL}/artworks/${id}`),

  // Places
  getAllPlaces: async () => axios.get(`${API_BASE_URL}/places`),
  getPlaceById: async (id: number) => axios.get(`${API_BASE_URL}/places/${id}`),
  createPlace: async (placeData: any) => axios.post(`${API_BASE_URL}/places`, placeData),
  updatePlace: async (id: number, placeData: any) => axios.put(`${API_BASE_URL}/places/${id}`, placeData),
  deletePlace: async (id: number) => axios.delete(`${API_BASE_URL}/places/${id}`),

  // Performances
  getAllPerformances: async () => axios.get(`${API_BASE_URL}/performances`),
  getPerformanceById: async (id: number) => axios.get(`${API_BASE_URL}/performances/${id}`),
  createPerformance: async (performanceData: any) => axios.post(`${API_BASE_URL}/performances`, performanceData),
  updatePerformance: async (id: number, performanceData: any) => axios.put(`${API_BASE_URL}/performances/${id}`, performanceData),
  deletePerformance: async (id: number) => axios.delete(`${API_BASE_URL}/performances/${id}`),

  // Events
  getAllEvents: async () => axios.get(`${API_BASE_URL}/events`),
  getEventById: async (id: number) => axios.get(`${API_BASE_URL}/events/${id}`),
  createEvent: async (eventData: any) => axios.post(`${API_BASE_URL}/events`, eventData),
  updateEvent: async (id: number, eventData: any) => axios.put(`${API_BASE_URL}/events/${id}`, eventData),
	deleteEvent: async (id: number) => axios.delete(`${API_BASE_URL}/events/${id}`),

	// EventArtworks
	getAllEventArtworks: async () => axios.get(`${API_BASE_URL}/eventartworks`),
	getEventArtworkById: async (eventId: number, artworkId: number) => axios.get(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`),
	createEventArtwork: async (eventArtworkData: any) => axios.post(`${API_BASE_URL}/eventartworks`, eventArtworkData),
	updateEventArtwork: async (eventId: number, artworkId: number, eventArtworkData: any) => axios.put(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`, eventArtworkData),
	deleteEventArtwork: async (eventId: number, artworkId: number) => axios.delete(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`),

  // Images
  getAllImages: async () => axios.get(`${API_BASE_URL}/images`),
  getImageById: async (id: number) => axios.get(`${API_BASE_URL}/images/${id}`),
  createImage: async (imageData: any) => axios.post(`${API_BASE_URL}/images`, imageData),
  updateImage: async (id: number, imageData: any) => axios.put(`${API_BASE_URL}/images/${id}`, imageData),
  deleteImage: async (id: number) => axios.delete(`${API_BASE_URL}/images/${id}`),

  // Sentences
  getAllSentences: async () => axios.get(`${API_BASE_URL}/sentences`),
  getSentenceById: async (id: number) => axios.get(`${API_BASE_URL}/sentences/${id}`),
  createSentence: async (sentenceData: any) => axios.post(`${API_BASE_URL}/sentences`, sentenceData),
  updateSentence: async (id: number, sentenceData: any) => axios.put(`${API_BASE_URL}/sentences/${id}`, sentenceData),
  deleteSentence: async (id: number) => axios.delete(`${API_BASE_URL}/sentences/${id}`),

  // Ajoutez ici d'autres fonctions pour les autres entités si nécessaire
};

export default apiService;
