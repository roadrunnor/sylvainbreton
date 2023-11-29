var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
import axios from 'axios';
import config from '../config/config';
const API_BASE_URL = config.API_BASE_URL;
const apiService = {
    // Artworks
    getAllArtworks: () => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/artworks`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    getArtworkById: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/artworks/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    createArtwork: (artworkData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.post(`${API_BASE_URL}/artworks`, artworkData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    updateArtwork: (id, artworkData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.put(`${API_BASE_URL}/artworks/${id}`, artworkData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    deleteArtwork: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.delete(`${API_BASE_URL}/artworks/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    // Places
    getAllPlaces: () => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/places`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    getPlaceById: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/places/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    createPlace: (placeData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.post(`${API_BASE_URL}/places`, placeData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    updatePlace: (id, placeData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.put(`${API_BASE_URL}/places/${id}`, placeData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    deletePlace: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.delete(`${API_BASE_URL}/places/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    // Performances
    getAllPerformances: () => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/performances`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    getPerformanceById: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/performances/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    createPerformance: (performanceData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.post(`${API_BASE_URL}/performances`, performanceData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    updatePerformance: (id, performanceData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.put(`${API_BASE_URL}/performances/${id}`, performanceData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    deletePerformance: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.delete(`${API_BASE_URL}/performances/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    // Events
    getAllEvents: () => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/events`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    getEventById: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/events/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    createEvent: (eventData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.post(`${API_BASE_URL}/events`, eventData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    updateEvent: (id, eventData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.put(`${API_BASE_URL}/events/${id}`, eventData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    deleteEvent: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.delete(`${API_BASE_URL}/events/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    // EventArtworks
    getAllEventArtworks: () => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/eventartworks`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    getEventArtworkById: (eventId, artworkId) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    createEventArtwork: (eventArtworkData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.post(`${API_BASE_URL}/eventartworks`, eventArtworkData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    updateEventArtwork: (eventId, artworkId, eventArtworkData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.put(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`, eventArtworkData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    deleteEventArtwork: (eventId, artworkId) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.delete(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    // Images
    getAllImages: () => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/images`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    getImageById: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/images/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    createImage: (imageData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.post(`${API_BASE_URL}/images`, imageData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    updateImage: (id, imageData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.put(`${API_BASE_URL}/images/${id}`, imageData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    deleteImage: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.delete(`${API_BASE_URL}/images/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    // Sentences
    getAllSentences: () => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/sentences`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    getSentenceById: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.get(`${API_BASE_URL}/sentences/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    createSentence: (sentenceData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.post(`${API_BASE_URL}/sentences`, sentenceData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    updateSentence: (id, sentenceData) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.put(`${API_BASE_URL}/sentences/${id}`, sentenceData);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    deleteSentence: (id) => __awaiter(void 0, void 0, void 0, function* () {
        try {
            const response = yield axios.delete(`${API_BASE_URL}/sentences/${id}`);
            return response.data;
        }
        catch (error) {
            handleError(error);
        }
    }),
    // Ajoutez ici d'autres fonctions pour les autres entités si nécessaire
};
function handleError(error) {
    if (axios.isAxiosError(error)) {
        console.error('Erreur Axios :', error.response);
    }
    else {
        console.error('Erreur inattendue :', error);
    }
    throw error;
}
export default apiService;
//# sourceMappingURL=apiService.js.map