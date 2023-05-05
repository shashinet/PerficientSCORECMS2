/* eslint-disable max-len */
/* eslint-disable no-unused-vars */
/* eslint-disable react/jsx-boolean-value */
/* eslint-disable import/no-unresolved */
/* eslint-disable no-undef */
// eslint-disable-next-line no-unused-vars
import React, { useState, useEffect, useRef } from 'react';
import PropTypes, { string } from 'prop-types';
import { Swiper, SwiperSlide } from 'swiper/react';
import SwiperCore, { Autoplay, Navigation, Keyboard, Pagination } from 'swiper';
import useWindowSize from '../../../hooks/useWindowSize';
import styles from './DefaultSlider.module.scss';
import Card from '../../Card';
import ButtonLink from '../../ButtonLink';

SwiperCore.use([Autoplay, Navigation, Keyboard, Pagination]);

export default function DefaultSlider(props) {
  const { cards, title, titleTag, callToActionButtons, showPagination, globalStyle } = props;
  const { width } = useWindowSize();

  const spaceBetween = 16;
  const showDots = width < 992;

  return (
    <>
      <section className={[styles.sliderBlock, ...globalStyle].join('')}>
        <div className="container">
          <div className="w-full">
            <div className="section-header">
              <div className="title">
                {titleTag === 'H2' && <h2>{title}</h2>}
                {titleTag === 'H3' && <h3>{title}</h3>}
                {titleTag === 'H4' && <h4>{title}</h4>}
              </div>
            </div>
            <div className="cta-area">
              {callToActionButtons.map((button) => (
                <>
                  <ButtonLink buttonStyles={[button.style]} title={button.text} url={button.url} />
                </>
              ))}
            </div>
            <Swiper
              spaceBetween={spaceBetween}
              grid={{ rows: 1 }}
              autoplay={false}
              navigation={true}
              centerInsufficientSlides={true}
              keyboard={true}
              pagination={showDots}
              // eslint-disable-next-line react/jsx-curly-brace-presence
              slidesPerView={'auto'}
              className="Myswiper"
            >
              {/* eslint-disable-next-line no-unused-vars */}
              {React.Children.toArray(
                cards.map((card) => (
                  <>
                    <SwiperSlide>
                      <Card
                        image={card.image}
                        body={card.body}
                        cardStyles={card.cardStyle}
                        h2={card.titleTag === 'H2' && card.title}
                        h3={card.titleTag === 'H3' && card.title}
                        h4={card.titleTag === 'H4' && card.title}
                        h5={card.titleTag === 'H5' && card.title}
                        subHeading={card.bodyDescription}
                        cta={React.Children.toArray(
                          card.callToAction.map((button) => (
                            <>
                              <ButtonLink buttonStyles={[button.style]} title={button.text} url={button.url} />
                            </>
                          )),
                        )}
                      />
                    </SwiperSlide>
                  </>
                )),
              )}
            </Swiper>
          </div>
        </div>
      </section>
    </>
  );
}

DefaultSlider.propTypes = {
  title: PropTypes.string,
  titleTag: PropTypes.string,
  cards: PropTypes.arrayOf(
    PropTypes.shape({
      cardStyle: PropTypes.arrayOf(PropTypes.string),
      title: PropTypes.string,
      titleTag: PropTypes.string,
      image: PropTypes.shape({
        contentType: PropTypes.string,
        imageSrc: PropTypes.string,
        altText: PropTypes.string,
      }),
      bodyDescription: PropTypes.string,
      body: PropTypes.string,
      callToAction: PropTypes.arrayOf(
        PropTypes.shape({
          style: PropTypes.string,
          text: PropTypes.string,
          url: PropTypes.string,
          openInNewWindow: PropTypes.bool,
          contentType: PropTypes.string,
          globalStyle: PropTypes.arrayOf(PropTypes.string),
        }),
      ),
      contentType: PropTypes.string,
      globalStyle: PropTypes.arrayOf(PropTypes.string),
    }),
  ),
  showPagination: PropTypes.bool,
  globalStyle: PropTypes.arrayOf(PropTypes.string),
  callToActionButtons: PropTypes.arrayOf(
    PropTypes.shape({
      style: PropTypes.string,
      text: PropTypes.string,
      url: PropTypes.string,
      openInNewWindow: PropTypes.bool,
      contentType: PropTypes.string,
      globalStyle: PropTypes.arrayOf(PropTypes.string),
    }),
  ),
};

DefaultSlider.defaultProps = {
  title: '',
  titleTag: '',
  callToActionButtons: [],
  cards: [],
  showPagination: false,
  globalStyle: [],
};
