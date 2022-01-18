import{_ as w,A as c,a as K,p as u,l as x,P as k}from"./index.ade1054c.js";import{E as C,G as S,c as l,a as d,o as a,b as o,d as e,t as p,F as _,y as m,z as A,e as i,p as M,m as I,C as h}from"./vendor.2a3cb938.js";const N={name:"Account",setup(){const n=c.account,r=C();return S(async()=>{try{r.params.id===n.id?await K.getAccountVaults():await u.getProfileVaults(r.params.id),await u.setActiveProfile(r.params.id),await u.getProfileKeeps(r.params.id)}catch(f){x.error(f),k.toast("Something went wrong!","error")}}),{route:r,accountVaults:l(()=>c.accountVaults),account:l(()=>c.account),myProfile:l(()=>c.myProfile),user:l(()=>c.user),activeProfile:l(()=>c.activeProfile),profileKeeps:l(()=>c.profileKeeps),profileVaults:l(()=>c.profileVaults)}}},g=n=>(M("data-v-53578f0c"),n=n(),I(),n),B={class:"about p-5"},R={class:"row"},j={class:"col-md-3"},E=["src"],F={class:"col-lg-9"},O={class:"mb-4"},z={class:""},D={class:"mb-4"},G={class:"row"},L={class:"col"},T=g(()=>e("h2",null,[h(" Vaults"),e("button",{class:"btn mdi mdi-plus mdi-24px text-secondary","data-bs-toggle":"modal","data-bs-target":"#createVault"})],-1)),q={key:0,class:"row"},H={key:1,class:"row"},J={class:"row"},Q={class:"col"},U=g(()=>e("h2",null,[h(" Keeps"),e("button",{class:"btn mdi mdi-plus mdi-24px text-secondary","data-bs-toggle":"modal","data-bs-target":"#createKeep"})],-1)),W={class:"row"},X={key:0,class:"mdi mdi-account floating-btn-right","data-bs-toggle":"offcanvas","data-bs-target":"#offcanvasRight","aria-controls":"offcanvasRight"};function Y(n,r,f,t,Z,$){const v=d("Vault"),V=d("Keep"),b=d("AccountOffCanvas"),y=d("CreateKeepModal"),P=d("CreateVaultModal");return a(),o(_,null,[e("div",B,[e("div",R,[e("div",j,[e("img",{class:"rounded mb-5",src:t.activeProfile.picture,alt:""},null,8,E)]),e("div",F,[e("h1",O,p(t.activeProfile.name),1),e("h3",z,"Vaults: "+p(t.profileVaults.length),1),e("h3",D,"Keeps: "+p(t.profileKeeps.length),1)])]),e("div",G,[e("div",L,[T,t.route.params.id!==t.account.id?(a(),o("div",q,[(a(!0),o(_,null,m(t.profileVaults,s=>(a(),o("div",{class:"col-md-3",key:s.id},[i(v,{vault:s},null,8,["vault"])]))),128))])):(a(),o("div",H,[(a(!0),o(_,null,m(t.accountVaults,s=>(a(),o("div",{class:"col-md-3",key:s.id},[i(v,{vault:s},null,8,["vault"])]))),128))]))])]),e("div",J,[e("div",Q,[U,e("div",W,[(a(!0),o(_,null,m(t.profileKeeps,s=>(a(),o("div",{class:"col-md-2",key:s.id},[i(V,{keep:s},null,8,["keep"])]))),128))])])])]),t.myProfile.id===t.activeProfile.id?(a(),o("button",X)):A("",!0),i(b),i(y),i(P)],64)}var ae=w(N,[["render",Y],["__scopeId","data-v-53578f0c"]]);export{ae as default};
