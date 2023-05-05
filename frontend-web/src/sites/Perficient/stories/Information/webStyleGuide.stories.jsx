import React from 'react';
import Swatch from '../../../../global/components/ColorSwatch/colorSwatch';
import ButtonLink from '../../../../core/react/ButtonLink';
import RichText from '../../../../core/react/RichText/richText';
import Stripe from '../../../../core/react/Stripe';
import DocumentHeading from '../../../../core/react/DocumentHeader';

export default {
  title: 'Information/WebStyleGuide',
  parameters: {
    layout: 'fullscreen',
  },
};

// Buttons
const buttons = [
  {
    name: 'primary',
    label: 'Primary'
  },
  {
    name: 'secondary',
    label: 'Secondary'
  },
  {
    name: 'task',
    label: 'Task'
  },
];

const overdark = [
  {
    name: 'over-dark',
    label: 'Over Dark'
  },
  {
    name: 'white',
    label: 'White'
  },
];

const colors = [
  {
    name: 'Primary',
    bg: '--primary',
    hex: '#010101'
  },
  {
    name: 'Secondary',
    bg: '--secondary',
    hex: '#CC1F20'
  },
  {
    name: 'Secondary Light',
    bg: '--secondaryLight',
    hex: '#E61717'
  },
  {
    name: 'Secondary Dark',
    bg: '--secondaryDark',
    hex: '#8D0E11'
  },
  {
    name: 'Tertiary',
    bg: '--tertiary',
    hex: '#B79967'
  },
  {
    name: 'Tertiary Light',
    bg: '--tertiaryLight',
    hex: '#C9B38D'
  },
  {
    name: 'Tertiary Dark',
    bg: '--tertiaryDark',
    hex: '#8C734B'
  },
  {
    name: 'Orange',
    bg: '--orange',
    hex: '#ea7600'
  },
  {
    name: 'Green',
    bg: '--green',
    hex: '#919130'
  },
  {
    name: 'Yellow',
    bg: '--yellow',
    hex: '#f0b323'
  },
  {
    name: 'Blue',
    bg: '--blue',
    hex: '#3D5D6F'
  },
  {
    name: 'Dark Grey',
    bg: '--darkgrey',
    hex: '#222222'
  },
  {
    name: 'Mid Grey1',
    bg: '--midgrey1',
    hex: '#444444'
  },
  {
    name: 'Mid Grey2',
    bg: '--midgrey2',
    hex: '#58595B'
  },
  {
    name: 'Mid Grey3',
    bg: '--midgrey3',
    hex: '#9E9E9E'
  },
  {
    name: 'Light Grey',
    bg: '--lightgrey',
    hex: '#E8E8E8'
  },
  {
    name: 'Black',
    bg: '--black',
    hex: '#010101'
  },
  {
    name: 'White',
    bg: '--white',
    hex: '#FFFFFF'
  },
];

// Font colors
const fontColors = [
  {
    name: 'Title',
    bg: '--title',
    hex: '#CC1F20'
  },
  {
    name: 'Body',
    bg: '--body',
    hex: '#222222'
  },
  {
    name: 'Primary',
    bg: '--primary',
    hex: '#010101'
  },
  {
    name: 'Secondary',
    bg: '--secondary',
    hex: '#CC1F20'
  },
  {
    name: 'Secondary Light',
    bg: '--secondaryLight',
    hex: '#E61717'
  },
  {
    name: 'Secondary Dark',
    bg: '--secondaryDark',
    hex: '#8D0E11'
  },
  {
    name: 'Tertiary',
    bg: '--tertiary',
    hex: '#B79967'
  },
  {
    name: 'Grey',
    bg: '--midgrey2',
    hex: '#58595B'
  },
  {
    name: 'Links',
    bg: '--links',
    hex: '#CC1F20'
  },
  {
    name: 'White',
    bg: '--white',
    hex: '#FFFFFF'
  },
];

export const webStyleGuide = () => (
  // eslint-disable-next-line no-unused-vars
  // eslint-disable-next-line implicit-arrow-linebreak
  <div className="web-styleguide">
    <Stripe>
      <div className="w-full">
        <DocumentHeading h2="Web Style Guide"/>
      </div>
    </Stripe>
    <Stripe selections="mb-14">
      <div className="w-full">
        <RichText children={'<h3>Colors</h3><hr className="text-titlefont"/>'}/>
        <div
          style={{
            display: 'flex',
            flexWrap: 'wrap',
            marginBottom: '2rem',
            marginTop: '1.5rem',
          }}
        >
          {colors.map((color) => (
            <Swatch key={color.bg} name={color.name} hex={color.hex} bg={color.bg}/>
          ))}
        </div>
        <RichText children={'<h3>Font Colors</h3><hr className="text-titlefont"/>'}/>
        <div
          style={{
            display: 'flex',
            flexWrap: 'wrap',
            marginBottom: '2rem',
            marginTop: '1.5rem',
          }}
        >
          {fontColors.map((color) => (
            <Swatch key={color.bg} name={color.name} hex={color.hex} bg={color.bg}/>
          ))}
        </div>
      </div>
    </Stripe>
    <Stripe selections={['mb-14']}>
      <div className="w-full">
        <div
          style={{
            display: 'flex',
            flexWrap: 'wrap',
            gap: '160px',
            marginBottom: '2rem',
          }}
        >
          <RichText
            children={'<h3 className="heading-three">Desktop Typography</h3><hr className="text-title"/><h2 className="heading-one">H1 Heading</h1><h2 className="text-h2lg">H2 Heading</h2><h3 className="text-h3lg">H3 Heading</h3><h4 className="text-h4lg">H4 Heading</h4><h5 className="text-h5lg">H5 Heading</h5><p className="text-bodylg">Lorem ipsum dolor sit amet, consectetur adipiscing elit</p><ul><li className="text-bodylg">Lorem ipsum</li><li className="text-bodylg">Lorem ipsum</li><li className="text-bodylg">Lorem ipsum</li></ul>'}/>
          <RichText
            children={'<h3 className="heading-three">Mobile Typography</h3>\<hr className="text-title"/><h2 className="heading-one">H1 Heading</h2><h2 className="text-h2sm">H2 Heading</h2><h3 className="text-h3sm">H3 Heading</h3><h4 className="text-h4sm">H4 Heading</h4><h5 className="text-h5sm">H5 Heading</h5><p className="text-bodysm">Lorem ipsum dolor sit amet, consectetur adipiscing elit</p><ul><li className="text-bodysm">Lorem ipsum</li><li className="text-bodysm">Lorem ipsum</li><li className="text-bodysm">Lorem ipsum</li></ul>'}/>
        </div>
      </div>
    </Stripe>
    <Stripe selections={['mb-14']}>
      <div className="w-full">
        <RichText children={'<h3>Links & Buttons</h3><hr/>'}/>
        <div
          style={{
            display: 'flex',
            flexWrap: 'wrap',
            gap: '160px',
            marginBottom: '2rem',
          }}
        >
          <RichText children={'<p><a href="/">Default body link</a></p>'}/>
          <div>
            {buttons.map((button, index) => (
              <div key={index} className="mb-4">
                <ButtonLink label={button.label} selections={button.name}/>
              </div>
            ))}
          </div>
          <div className="py-4 bg-primaryblue">
            {overdark.map((button, index) => (
              <div key={index} className="m-4">
                <ButtonLink label={button.label} selections={button.name}/>
              </div>
            ))}
          </div>
        </div>
      </div>
    </Stripe>
    <Stripe selections={['mb-14']}>
      <div className="w-full">
        <RichText children={'<h3>Inputs</h3><hr className="mb-4"/>'}/>
        <div>
          <form className="flex" action="./home" method="post">
            <div className="fifty">
              <div className="form-control">
                <label htmlFor="name">Text Input</label>
                <input type="text" id="name" name="user_name" placeholder="Placeholder Text"/>
              </div>
              <div className="form-control select">
                <label htmlFor="12">Dropdown</label>
                <select
                  id="12"
                  name="change-options"
                  required=""
                  aria-required="true"
                  aria-describedby="dropdowns"
                  aria-invalid="true"
                >
                  <option disabled>Select</option>
                  <option>First Option</option>
                  <option>Second Option</option>
                  <option>Third Option</option>
                  <option>Fourth Option</option>
                </select>
              </div>
              <div className="form-control">
                <label htmlFor="typed">Text Input</label>
                <input type="text" value="Typed Text" id="typed" name="user_name"/>
              </div>
            </div>
            <div className="mr-8">
              <legend>Radio Buttons</legend>
              <div className="form-control radio">
                <label htmlFor="radio1">
                  <input type="radio" value="radio1" id="radio1"/>
                  <span>Unselected</span>
                </label>
              </div>
              <div className="form-control radio">
                <label htmlFor="radio2">
                  <input type="radio" value="radio2" id="radio2" checked/>
                  <span>Selected</span>
                </label>
              </div>
            </div>
            <div>
              <legend>Checkbox</legend>
              <div className="form-control checkbox">
                <label htmlFor="checkbox1">
                  <input type="checkbox" value="checkbox1" id="checkbox1"/>
                  <span> Unselected</span>
                </label>
              </div>
              <div className="form-control checkbox">
                <label htmlFor="checkbox2">
                  <input type="checkbox" value="checkbox2" id="checkbox2" checked/>
                  <span>Selected</span>
                </label>
              </div>
            </div>
          </form>
        </div>
        <div className="p-8 bg-secondaryDark text-white">
          <form className="flex" action="./home" method="post">
            <div className="fifty">
              <div className="form-control">
                <label htmlFor="name">Text Input</label>
                <input type="text" id="name" name="user_name" placeholder="Placeholder Text"/>
              </div>
              <div className="form-control">
                <label htmlFor="12">Dropdown</label>
                <select
                  id="12"
                  name="change-options"
                  required=""
                  aria-required="true"
                  aria-describedby="dropdowns"
                  aria-invalid="true"
                >
                  <option disabled>Select</option>
                  <option>First Option</option>
                  <option>Second Option</option>
                  <option>Third Option</option>
                  <option>Fourth Option</option>
                </select>
              </div>
              <div className="form-control">
                <label htmlFor="typed">Text Input</label>
                <input type="text" value="Typed Text" id="typed" name="user_name"/>
              </div>
            </div>
            <div className="mr-8">
              <legend>Radio Buttons</legend>
              <div className="form-control radio">
                <label htmlFor="radio3">
                  <input type="radio" value="radio3" id="radio3"/>
                  <span>Unselected</span>
                </label>
              </div>
              <div className="form-control radio">
                <label htmlFor="radio4">
                  <input type="radio" value="radio4" id="radio4" checked/>
                  <span>Selected</span>
                </label>
              </div>
            </div>
            <div>
              <legend>Checkbox</legend>
              <div className="form-control checkbox">
                <label htmlFor="checkbox3">
                  <input type="checkbox" value="checkbox3" id="checkbox3"/>
                  <span> Unselected</span>
                </label>
              </div>
              <div className="form-control checkbox">
                <label htmlFor="checkbox4">
                  <input type="checkbox" value="checkbox4" id="checkbox4" checked/>
                  <span>Selected</span>
                </label>
              </div>
            </div>
          </form>
        </div>
      </div>
    </Stripe>
  </div>
);
