import React from "react";
import "../../scss/_buttons.scss";
import arrowNorthEast400 from "assets/icons/arrow-north-east400.svg";

const ButtonShopNow = () => (
	<div className="btn-fe">
		<a href="" rel="noopener" target="_self" aria-label="Start now" className="uiLink">
			<span className="flexContainer">
				<span>Shop now</span>
				<svg className="arrowIcon" fill="none" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" aria-hidden="true">
					<polygon fill="currentColor" points="5 4.31 5 5.69 9.33 5.69 2.51 12.51 3.49 13.49 10.31 6.67 10.31 11 11.69 11 11.69 4.31 5 4.31"></polygon>
				</svg>
			</span>
		</a>
	</div>
);

export default ButtonShopNow;
