export interface ImageData {
	ImageID: number; // Assuming ImageID should be included and is a number
	ArtworkId?: number; // Nullable number to match int? on server-side
	PerformanceId?: number; // Nullable number to match int? on server-side
	FileName: string; // Assuming this should match FileRoute from server-side
	Description: string;
	MediaType: string;
	MediaDescription: string;
	// You can include other server-side properties as needed
}
