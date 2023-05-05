/* eslint-disable react/jsx-boolean-value */
/* eslint-disable import/no-unresolved */
/* eslint-disable no-undef */
// eslint-disable-next-line no-unused-vars
import React, { useState, useEffect, useRef } from 'react';
import PropTypes from 'prop-types';

// Swiper
import { Swiper, SwiperSlide } from 'swiper/react';
import SwiperCore, { AutoPlay, Navigation, Keyboard } from 'swiper';

// children
import Components from '../ComponentFactory/components';

// Swiper modules
SwiperCore.use([AutoPlay, Navigation, Keyboard]);

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function Slider(props) {
  const { slides, spaceBetween } = props;

  return (
    <>
      <Swiper
        spaceBetween={spaceBetween}
        grid={{ rows: 1 }}
        autoplay={false}
        navigation={true}
        centerInsufficientSlides={true}
        keyboard={true}
        // eslint-disable-next-line react/jsx-curly-brace-presence
        slidesPerView={'auto'}
        className="Myswiper"
      >
        {slides.map((result) => (
          <SwiperSlide key={result.id}>
            {Components(block)}
          </SwiperSlide>
        ))}
      </Swiper>
    </>
  );
}

Slider.propTypes = {
  spaceBetween: PropTypes.number,
  slides: PropTypes.arrayOf(
    PropTypes.shape({}),
  ).isRequired,
};

Slider.defaultProps = {
  spaceBetween: 24,
};
