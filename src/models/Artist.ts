export interface Artist {
	ArtistID: number;
	FirstName: string;
	LastName: string;
}

// Define an interface for the API response if needed
export interface ApiResponse<T> {
	data: T;
	// ... any other relevant fields that your API responses contain
}
