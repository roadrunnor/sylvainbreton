import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000/api';

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
      const response = await axios.post(`${API_BASE_URL}/artworks`, artworkData);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  },
  updateArtwork: async (id: number, artworkData: any) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/artworks/${id}`, artworkData);
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
      const response = await axios.put(`${API_BASE_URL}/places/${id}`, placeData);
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
      const response = await axios.post(`${API_BASE_URL}/performances`, performanceData);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  },
  updatePerformance: async (id: number, performanceData: any) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/performances/${id}`, performanceData);
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
      const response = await axios.put(`${API_BASE_URL}/events/${id}`, eventData);
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
      const response = await axios.get(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  },
  createEventArtwork: async (eventArtworkData: any) => {
    try {
      const response = await axios.post(`${API_BASE_URL}/eventartworks`, eventArtworkData);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  },
  updateEventArtwork: async (eventId: number, artworkId: number, eventArtworkData: any) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`, eventArtworkData);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  },
  deleteEventArtwork: async (eventId: number, artworkId: number) => {
    try {
      const response = await axios.delete(`${API_BASE_URL}/eventartworks/${eventId}/${artworkId}`);
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
  updateImage: async (id: number, imageData: any) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/images/${id}`, imageData);
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
      return response.data;
    } catch (error) {
      handleError(error);
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
      const response = await axios.post(`${API_BASE_URL}/sentences`, sentenceData);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  },
  updateSentence: async (id: number, sentenceData: any) => {
    try {
      const response = await axios.put(`${API_BASE_URL}/sentences/${id}`, sentenceData);
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

  // Ajoutez ici d'autres fonctions pour les autres entités si nécessaire
};

function handleError(error: any) {
  if (axios.isAxiosError(error)) {
    console.error('Erreur Axios :', error.response);
  } else {
    console.error('Erreur inattendue :', error);
  }
  throw error;
}

export default apiService;
