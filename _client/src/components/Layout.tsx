import React, { useState, useEffect } from "react";
import "../scss/_layout.scss";
import { Artwork } from "../models/Artwork";
import { ArtworkImage } from "../models/ArtworkImage";
import { Category } from "../models/Category";
import { useApiService } from "../services/apiService";
import ButtonShopNow from "./buttons/ButtonShopNow";

const Layout = () => {
	const [artworks, setArtworks] = useState<Artwork[]>([]);
	const [categoryMap, setCategoryMap] = useState<{ [key: string]: string }>({});
	const { apiService } = useApiService();

	// Fisher-Yates Shuffle Algorithm
	const shuffleArray = (array: Artwork[]) => {
		for (let i = array.length - 1; i > 0; i--) {
			const j = Math.floor(Math.random() * (i + 1));
			[array[i], array[j]] = [array[j], array[i]];
		}
		return array;
	};

	useEffect(() => {
		const fetchArtworksAndCategories = async () => {
			const fetchedArtworks = await apiService.getAllArtworks();

			// Shuffle the artworks using the Fisher-Yates shuffle algorithm
			const shuffledArtworks = shuffleArray(fetchedArtworks);
			setArtworks(shuffledArtworks);

			const fetchedCategories = await apiService.getAllCategories();
			const newCategoryMap = fetchedCategories.reduce((accumulator: Record<number, string>, category: Category) => {
				accumulator[category.CategoryID] = category.CategoryName;
				return accumulator;
			}, {} as Record<number, string>);

			setCategoryMap(newCategoryMap);
		};

		fetchArtworksAndCategories();
	}, []);

	const getCategoryNameById = (categoryId: number) => {
		return categoryMap[categoryId] || "Unknown Category";
	};

	const imageBasePath = process.env.REACT_APP_IMAGE_PATH || "/assets/images/";
	const getImagePath = (artworkImages?: ArtworkImage[]) => {
		if (!artworkImages || artworkImages.length === 0 || !artworkImages[0].URL) {
			return `${imageBasePath}no-image.webp`;
		}
		return artworkImages[0].URL;
	};

	return (
		<div className="layout">
			{artworks.length > 0 && (
				<div className="image-row">
					<div className="single-image">
						<img key={artworks[0].ArtworkID} src={getImagePath(artworks[0].ArtworkImages)} alt={artworks[0].Description} />
					</div>
					<div className="image-info">
						<div className="col btn-fe">
							<ButtonShopNow />
						</div>
						<p className="image-info-padding-b">
							<em>{artworks[0].Title}</em> ({artworks[0].CreationDate.slice(0, 4)}), {artworks[0].Materials}
						</p>
						<p>{artworks[0].Description}</p>
						<p>
							{getCategoryNameById(artworks[0].CategoryID)}, {artworks[0].Conceptual}
						</p>
					</div>
				</div>
			)}
			<div className="gallery-row">
				{artworks.slice(1, 4).map((artwork) => (
					<div className="image-container" key={artwork.ArtworkID}>
						<img src={getImagePath(artwork.ArtworkImages)} alt={artwork.Description} />
						<div className="image-description">
							<div className="col btn-fe">
								<ButtonShopNow />
							</div>
							<p>
								<em>{artwork.Title}</em> ({artwork.CreationDate.slice(0, 4)}), {artwork.Materials}
							</p>

							<p>{artwork.Description}</p>
							<p>
								{getCategoryNameById(artwork.CategoryID)}, {artwork.Conceptual}
							</p>
						</div>
					</div>
				))}
			</div>
		</div>
	);
};

export default Layout;
