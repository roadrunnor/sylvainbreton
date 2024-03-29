import { ArtworkImage } from "./ArtworkImage";
export interface Artwork {
	ArtworkID: number;
	Title: string;
	CreationDate: string;
	CategoryID: number;
	CategoryName: string;
	Materials: string;
	Dimensions: string;
	Description: string;
	Conceptual: string;
	ArtworkImages?: ArtworkImage[];
}
