import React, { useState, useEffect } from "react";
import "../scss/_layout.scss";
import { Artwork } from "../models/Artwork";
import { Category } from "../models/Category";
import { useApiService } from "../services/apiService";

const Layout = () => {
	const [artworks, setArtworks] = useState<Artwork[]>([]);
	const [categoryMap, setCategoryMap] = useState<{ [key: string]: string }>({});
	const { apiService } = useApiService();

	useEffect(() => {
		const fetchArtworksAndCategories = async () => {
			const fetchedArtworks = await apiService.getAllArtworks();
			setArtworks(fetchedArtworks);

			const fetchedCategories = await apiService.getAllCategories();
			const newCategoryMap = fetchedCategories.reduce(
				(accumulator: Record<number, string>, category: Category) => {
					accumulator[category.CategoryID] = category.CategoryName;
					return accumulator;
				},
				{} as Record<number, string>
			);

			setCategoryMap(newCategoryMap);
		};

		fetchArtworksAndCategories();
	}, []);

	const getCategoryNameById = (categoryId: number) => {
		return categoryMap[categoryId] || "Unknown Category";
	};

	const imageBasePath = process.env.REACT_APP_IMAGE_PATH;
	const getImagePath = (fileName?: string) => {
		if (!fileName) {
			return `${imageBasePath}no-image.webp`;
		}
		return `${imageBasePath}${fileName}`;
	};

	return (
		<div className="layout">
			{artworks.length > 0 && (
				<div className="image-row">
					<div className="single-image">
						<img
							key={artworks[0].ArtworkID}
							src={getImagePath(artworks[0].FileName)}
							alt={artworks[0].Description}
						/>
					</div>
					<div className="image-info">
						<p className="image-info-padding-b">
							<em>{artworks[0].Title}</em>,{" "}
							{artworks[0].CreationDate.slice(0, 4)}, {artworks[0].CategoryName}
						</p>
						<p>{artworks[0].Description}</p>
						<p>{artworks[0].Conceptual}</p>
					</div>
				</div>
			)}
			<div className="gallery-row">
				{artworks.slice(1, 4).map((artwork) => (
					<div className="image-container" key={artwork.ArtworkID}>
						<img
							src={getImagePath(artwork.FileName)}
							alt={artwork.Description}
						/>
						<div className="image-description">
							<p>
								<em>{artwork.Title}</em>, {artwork.CreationDate.slice(0, 4)}, {artwork.CategoryName}
							</p>
							<p>{artwork.Description}</p>
							<p>{artwork.Conceptual}</p>
						</div>
					</div>
				))}
			</div>
		</div>
	);
};

export default Layout;
